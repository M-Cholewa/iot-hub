using Communication.Config;
using Domain.RabbitMQ.RPC;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Communication.DeviceConnection
{
    public class RabbitMQConnectionConfig
    {
        public string UriString { get; set; } = "?";
    }

    public enum RpcResult
    {
        SUCCESS,
        ERROR_TIMEOUT,
        ERROR_OTHER,
    }

    public class RpcClient : IRpcClient
    {
        private readonly IConnection connection;
        private readonly IModel channel;
        private readonly ConcurrentDictionary<string, TaskCompletionSource<RpcResponse?>> callbackMapper = new();
        private readonly ILogger<RpcClient> _logger;

        public RpcClient(RabbitMQConnectionConfig rabbitMQConnection, ILogger<RpcClient> logger)
        {
            _logger = logger;

            var factory = new RabbitMQ.Client.ConnectionFactory
            {
                Uri = new Uri(rabbitMQConnection.UriString)
            };

            connection = factory.CreateConnection();
            channel = connection.CreateModel();

            // create reply queue
            channel.QueueDeclare(queue: RabbitMQConfig.RPC_REPLY_QUEUE_NAME);
            // bind to mqtt
            channel.QueueBind(queue: RabbitMQConfig.RPC_REPLY_QUEUE_NAME,
              exchange: RabbitMQConfig.MQTT_EXCHANGE,
              routingKey: RabbitMQConfig.RPC_REPLY_QUEUE_NAME);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {

                var body = ea.Body.ToArray();
                var responseJson = Encoding.UTF8.GetString(body);
                var response = JsonSerializer.Deserialize<RpcResponse?>(responseJson);

                if (!callbackMapper.TryRemove(response?.CorelationId ?? "?", out var tcs))
                    return;

                tcs.TrySetResult(response);
            };

            channel.BasicConsume(consumer: consumer,
                                 queue: RabbitMQConfig.RPC_REPLY_QUEUE_NAME,
                                 autoAck: true);

        }

        public async Task<(RpcResult, RpcResponse?)> CallMethodAsync(string deviceId, string methodName, string argumentsJson, CancellationToken cancellationToken = default)
        {
            var correlationId = Guid.NewGuid().ToString();

            var task = Call(deviceId, methodName, argumentsJson, correlationId, cancellationToken);

            (RpcResult, RpcResponse?) result;

            //return response or timeout
            try
            {
                if (await Task.WhenAny(task, Task.Delay(RabbitMQConfig.RPC_TIMEOUT_MS, cancellationToken)) == task)
                {
                    result = (RpcResult.SUCCESS, await task);
                }
                else
                {
                    result = (RpcResult.ERROR_TIMEOUT, null);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("RpcClient CallMethodAsync" + ex.Message);
                result = (RpcResult.ERROR_OTHER, null);
            }

            // remove callback no matter what
            callbackMapper.TryRemove(correlationId, out _);
            return result;
        }

        private Task<RpcResponse?> Call(string deviceId, string methodName, string argumentsJson, string correlationId, CancellationToken cancellationToken = default)
        {
            RpcRequest req = new(methodName, argumentsJson, correlationId, RabbitMQConfig.RPC_REPLY_QUEUE_NAME);
            var tcs = new TaskCompletionSource<RpcResponse?>();
            callbackMapper.TryAdd(correlationId, tcs);

            channel.BasicPublish(exchange: RabbitMQConfig.MQTT_EXCHANGE,
                                 routingKey: deviceId,
                                 body: req.GetJsonBytes());

            cancellationToken.Register(() => callbackMapper.TryRemove(correlationId, out _));

            return tcs.Task;
        }

        public void Dispose()
        {
            channel.Close();
            connection.Close();
        }
    }
}
