﻿using System.Text;
using System.Text.Json;
using Business.Core.Auth.Commands;
using Business.InfluxRepository;
using Business.Repository;
using Communication.MQTT.Config;
using Domain.MQTT;
using MediatR;
using MessageProcessing.Messages.Requests;
using Microsoft.Extensions.Hosting;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;

namespace MessageProcessing.Services
{
    public class InterceptingService : IHostedService
    {

        private MQTTConnectionConfig _mqttConnectionConfig;
        private readonly IMediator _mediator;
        private IMqttClient? _mqttClient;

        public InterceptingService(MQTTConnectionConfig mqttConnectionConfig, IMediator mediator)
        {
            _mqttConnectionConfig = mqttConnectionConfig;
            _mediator = mediator;
        }

        /// <summary>
        /// Main program function
        /// </summary>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var mqttFactory = new MqttFactory();
            _mqttClient = mqttFactory.CreateMqttClient();

            var mqttClientOptions = new MqttClientOptionsBuilder()
                .WithTcpServer(_mqttConnectionConfig.ServerAddress)
                .WithClientId(_mqttConnectionConfig.ClientId.ToString())
                .WithCredentials(_mqttConnectionConfig.Login, _mqttConnectionConfig.Password)
            .Build();

            _mqttClient.ApplicationMessageReceivedAsync += async e =>
            {
                string clientIdString = e.ApplicationMessage.Topic.Split('/')[1];
                string action = e.ApplicationMessage.Topic.Split('/').Last();
                string payload = Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment);

                if (Guid.TryParse(clientIdString, out var clientId) == false)
                {
                    return;
                }


                if (action == "telemetry")
                {

                    var telemetry = JsonSerializer.Deserialize<List<BaseTelemetry>?>(payload);
                    var cmd = new AddTelemetryCommand() { ClientId = clientId, Telemetry = telemetry };

                    await _mediator.Send(cmd).ConfigureAwait(false);
                }

                if (action == "log")
                {
                    var log = JsonSerializer.Deserialize<List<LogTelemetry>?>(payload);
                    var cmd = new AddLogCommand() { ClientId = clientId, Telemetry = log };

                    await _mediator.Send(cmd).ConfigureAwait(false);
                }
            };

            var mqttSubscribeOptions = mqttFactory
                .CreateSubscribeOptionsBuilder()
                .WithTopicFilter(f => f.WithTopic("telemetry_queue/+/+"))
                .Build();

            // try to connect to the broker
            while (true)
            {
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
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("MessageProcessing Exited");

            if (_mqttClient != null && _mqttClient.IsConnected == true)
            {
                await _mqttClient.DisconnectAsync(cancellationToken: cancellationToken);
            }

        }

    }
}

