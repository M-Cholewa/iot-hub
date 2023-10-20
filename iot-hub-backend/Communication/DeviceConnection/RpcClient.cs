using Domain.RabbitMQ.RPC;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Communication.DeviceConnection
{
    public class RabbitMQConnectionConfig
    {
        public string UriString { get; set; } = "?";
    }

    public class RpcClient : IRpcClient
    {
        private const string QUEUE_NAME = "rpc_queue";

        private readonly IConnection connection;
        private readonly IModel channel;
        private readonly string replyQueueName;
        private readonly ConcurrentDictionary<string, TaskCompletionSource<RpcResponse?>> callbackMapper = new();

        public RpcClient(RabbitMQConnectionConfig rabbitMQConnection)
        {

            var factory = new RabbitMQ.Client.ConnectionFactory
            {
                Uri = new Uri(rabbitMQConnection.UriString)
            };

            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            // declare a server-named queue
            replyQueueName = channel.QueueDeclare().QueueName;

            // as the device is using MQTT and server is using AMQP, we must bind this queue to the default MQTT exchange which is amq.topic
            channel.QueueBind(queue: replyQueueName,
              exchange: "amq.topic",
              routingKey: replyQueueName);

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
                                 queue: replyQueueName,
                                 autoAck: true);
        }

        public Task<RpcResponse?> CallAsync(string message, CancellationToken cancellationToken = default)
        {
            var correlationId = Guid.NewGuid().ToString();
            RpcRequest req = new(replyQueueName, message, correlationId);
            var tcs = new TaskCompletionSource<RpcResponse?>();
            callbackMapper.TryAdd(correlationId, tcs);

            channel.BasicPublish(exchange: "amq.topic",
                                 routingKey: QUEUE_NAME,
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
