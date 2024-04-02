using Microsoft.Extensions.Hosting;
using MQTTnet.Server;
using MQTTnet;
using MQTTnet.Protocol;
using Business.Core.Auth.Commands;
using MediatR;
using MQTTServer.Infrastructure;
using Domain.InfluxDB;

namespace MQTTServer.Services
{
    public class ServerService : IHostedService
    {
        private MqttServer? mqttServer;
        private readonly IMediator _mediator;

        public ServerService(IMediator mediator)
        {
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

            mqttServer.ClientConnectedAsync += async e =>
            {
                //push telemetry message
                await mqttServer.SendStatusTelemetry(e.ClientId, Telemetries.STATUS_ONLINE);

                // push log message
                await mqttServer.SendStatusLog(e.ClientId, "Device connected");

            };

            mqttServer.ClientDisconnectedAsync += async e =>
            {
                //push telemetry message
                await mqttServer.SendStatusTelemetry(e.ClientId, Telemetries.STATUS_OFFLINE);

                // push log message
                await mqttServer.SendStatusLog(e.ClientId, "Device disconnected");
            };

            await mqttServer.StartAsync();
            Console.WriteLine("MQTT Server started...");
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

}

