using Communication.MQTT.Config;
using MQTTnet.Client;
using MQTTnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemTest.Base;
using MessageProcessing.Messages.Requests;
using Domain.MQTT;
using System.Text.Json;

namespace SystemTest.IntegrationTest.Services
{
    public class MQTTServer
    {
        private IMqttClient? _mqttClient;


        [Fact]
        public async Task BaseTest()
        {
            Task mqttServer = new MQTTServerFactory().Run();
            bool testSuccess = false;

            MQTTConnectionConfig _mqttConnectionConfig = new MQTTConnectionConfig
            {
                ServerAddress = "192.168.100.110",
                Password = "A5PM@]wB}rG}A*(F?1q>?1&X/61TavaNbh?:RDzW7]&Cz[xfP&R)gv&|c^iR",
                Login = "IotHubApi",
                ClientId = Guid.Parse("64f7f094-e0b9-4c52-935e-fd9d442d655e"),
            };


            var mqttFactory = new MqttFactory();
            _mqttClient = mqttFactory.CreateMqttClient();

            var mqttClientOptions = new MqttClientOptionsBuilder()
                .WithTcpServer(_mqttConnectionConfig.ServerAddress)
                .WithClientId(_mqttConnectionConfig.ClientId.ToString())
                .WithCredentials(_mqttConnectionConfig.Login, _mqttConnectionConfig.Password)
                .Build();

            _mqttClient.ApplicationMessageReceivedAsync += e =>
            {
                string action = e.ApplicationMessage.Topic.Split('/').Last();
                string payload = Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment);

                if (action == "telemetry")
                {
                    var telemetry = JsonSerializer.Deserialize<BaseTelemetry>(payload);
                    Assert.NotNull(telemetry);
                    Assert.Equal("test123!@#", telemetry.FieldName);

                    testSuccess = true;
                }


                return Task.CompletedTask;
            };

            var mqttSubscribeOptions = mqttFactory
                .CreateSubscribeOptionsBuilder()
                .WithTopicFilter(f => f.WithTopic("TEST_telemetry_queue/+/+"))
                .Build();

            // try to connect to the broker
            int connectionAttempts = 0;
            while (true)
            {
                if (connectionAttempts > 5)
                {
                    Assert.Fail();
                }

                try
                {
                    await _mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);
                    await _mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

                    Console.WriteLine($" [*] Connected to MQTT");

                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($" [*] Couldnt connect to MQTT" + ex.Message);
                    Thread.Sleep(2500);
                }

                connectionAttempts++;
            }

            var telemetry = new BaseTelemetry
            {
                FieldName = "test123!@#",
                Value = 1,
                Unit = "test"
            };

            var message = new MqttApplicationMessageBuilder()
                .WithTopic("TEST_telemetry_queue/64f7f094-e0b9-4c52-935e-fd9d442d655e/telemetry")
                .WithPayload(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(telemetry)))
                .Build();

            await _mqttClient.PublishAsync(message, CancellationToken.None);

            int timeout = 0;
            while (true)
            {
                if (timeout > 10)
                {
                    Assert.Fail();
                }

                if (testSuccess)
                {
                    break;
                }

                await Task.Delay(1000);
                timeout++;
            }
        }
    }
}
