using Communication.MQTT.Config;
using Communication.MQTT.Extensions;
using Domain.Core;
using Domain.InfluxDB;
using Domain.MQTT;
using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Exceptions;
using MQTTnet.Extensions.Rpc;
using MQTTnet.Protocol;
using MQTTnet.Server;
using System.Text.Json;
using System.Threading;

namespace Communication.MQTT
{
    public enum RpcResult
    {
        SUCCESS,
        ERROR_TIMEOUT,
        ERROR_OTHER,
    }

    public class RpcClient : IRpcClient
    {
        private readonly MQTTConnectionConfig _mqttConnectionConfig;
        private readonly ILogger<RpcClient> _logger;
        private readonly TimeSpan _executionTimeout = TimeSpan.FromMilliseconds(MQTTConfig.RPC_TIMEOUT_MS);

        public RpcClient(MQTTConnectionConfig mqttConnectionConfig, ILogger<RpcClient> logger)
        {
            _mqttConnectionConfig = mqttConnectionConfig;
            _logger = logger;
        }

        public async Task<(RpcResult, RpcResponse?)> CallMethodAsync(string deviceId, string methodName, string argumentsJson, CancellationToken cancellationToken = default)
        {
            RpcRequest req = new(argumentsJson);

            try
            {
                var mqttFactory = new MqttFactory();

                using var mqttClient = mqttFactory.CreateMqttClient();

                var mqttClientOptions = new MqttClientOptionsBuilder()
                    .WithTcpServer(_mqttConnectionConfig.ServerAddress)
                    .WithClientId(_mqttConnectionConfig.ClientId.ToString())
                    .WithCredentials(_mqttConnectionConfig.Login, _mqttConnectionConfig.Password)
                .Build();

                await mqttClient.ConnectAsync(mqttClientOptions, cancellationToken);

                using var mqttRpcClient = mqttFactory.CreateMqttRpcClient(mqttClient);

                // remotly execute function
                var responseJsonBytes = await mqttRpcClient.ExecuteAsync(_executionTimeout, deviceId, methodName, req.GetJsonString(), MqttQualityOfServiceLevel.AtMostOnce);

                // parse the response
                var response = JsonSerializer.Deserialize<RpcResponse?>(responseJsonBytes);

                _logger.LogInformation("The RPC call was successful.");

                return (RpcResult.SUCCESS, response);
            }
            catch (MqttCommunicationTimedOutException)
            {
                return (RpcResult.ERROR_TIMEOUT, null);
            }
            catch (Exception ex)
            {
                _logger.LogError($"RPC call ended with error {ex.Message}");
                return (RpcResult.ERROR_OTHER, null);
            }
        }
    }
}
