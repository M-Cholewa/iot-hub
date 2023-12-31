﻿using Microsoft.Extensions.Hosting;
using MQTTnet.Server;
using MQTTnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MQTTnet.Protocol;
using Business.Core.Auth.Commands;
using MediatR;

public class MQTTServer : IHostedService
{
    private MqttServer? mqttServer;
    private ManualResetEvent _exitEvent;
    private readonly IMediator _mediator;

    public MQTTServer(IMediator mediator)
    {
        _exitEvent = new ManualResetEvent(false);

        Console.CancelKeyPress += (sender, eArgs) =>
        {
            _exitEvent.Set();
        };
        _mediator = mediator;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        /*
         * See _Run_Minimal_Server_ for more information.
         */
        Console.WriteLine("Starting MQTT Server...");

        var mqttFactory = new MqttFactory();

        var mqttServerOptions = new MqttServerOptionsBuilder().WithDefaultEndpoint().Build();

        mqttServer = mqttFactory.CreateMqttServer(mqttServerOptions);
        // Setup connection validation before starting the server so that there is 
        // no change to connect without valid credentials.
        mqttServer.ValidatingConnectionAsync += async e =>
        {
            // Parse string to Guid
            if (Guid.TryParse(e.ClientId, out var clientId) == false)
            {
                e.ReasonCode = MqttConnectReasonCode.ClientIdentifierNotValid;
                return;
            }


            var cmd = new MQTTLoginCommand() { ClientId = clientId, Username = e.UserName, Password = e.Password };
            var result = await _mediator.Send(cmd).ConfigureAwait(false);

            e.ReasonCode = result.MqttConnectReasonCode;
        };


        await mqttServer.StartAsync();
        Console.WriteLine("MQTT Server started...");
        Console.WriteLine(" Press [CTRL+C] to exit.");

    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (mqttServer != null)
        {
            await mqttServer.StopAsync();
        }

        Console.WriteLine("MqttServer Exited");
    }
}
