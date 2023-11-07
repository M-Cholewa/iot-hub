using Microsoft.Extensions.Hosting;
using MQTTnet.Server;
using MQTTnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MQTTnet.Protocol;

public class MQTTServer : IHostedService
{
    private ManualResetEvent _exitEvent;

    public MQTTServer()
    {

    }
    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Validating_Connections();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public static async Task Validating_Connections()
    {
        /*
         * This sample starts a simple MQTT server which will check for valid credentials and client ID.
         *
         * See _Run_Minimal_Server_ for more information.
         */

        var mqttFactory = new MqttFactory();

        var mqttServerOptions = new MqttServerOptionsBuilder().WithDefaultEndpoint().Build();

        using (var mqttServer = mqttFactory.CreateMqttServer(mqttServerOptions))
        {
            // Setup connection validation before starting the server so that there is 
            // no change to connect without valid credentials.
            mqttServer.ValidatingConnectionAsync += e =>
            {
                //if (e.ClientId != "ValidClientId")
                //{
                //    e.ReasonCode = MqttConnectReasonCode.ClientIdentifierNotValid;
                //}

                if (e.UserName != "mqtt-test" && e.UserName != "user")
                {
                    e.ReasonCode = MqttConnectReasonCode.BadUserNameOrPassword;
                }

                if (e.Password != "mqtt-test" && e.Password != "mypass")
                {
                    e.ReasonCode = MqttConnectReasonCode.BadUserNameOrPassword;
                }

                return Task.CompletedTask;
            };


            await mqttServer.StartAsync();

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await mqttServer.StopAsync();
        }
    }
}
