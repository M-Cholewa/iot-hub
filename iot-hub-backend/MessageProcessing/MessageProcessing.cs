using System.Text;
using Communication.MQTT.Config;
using Microsoft.Extensions.Hosting;
using MQTTnet;
using MQTTnet.Client;

public class MessageProcessing : IHostedService
{

    private ManualResetEvent _exitEvent;
    private MQTTConnectionConfig _mqttConnectionConfig;

    public MessageProcessing(MQTTConnectionConfig mqttConnectionConfig)
    {
        _mqttConnectionConfig = mqttConnectionConfig;
        _exitEvent = new ManualResetEvent(false);

        Console.CancelKeyPress += (sender, eArgs) =>
        {
            _exitEvent.Set();
        };
    }

    /// <summary>
    /// Main program function
    /// </summary>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var mqttFactory = new MqttFactory();
        IMqttClient _mqttClient = mqttFactory.CreateMqttClient();


        var mqttClientOptions = new MqttClientOptionsBuilder()
            .WithTcpServer(_mqttConnectionConfig.ServerAddress)
            .WithClientId(_mqttConnectionConfig.ClientId.ToString())
            .WithCredentials(_mqttConnectionConfig.Login, _mqttConnectionConfig.Password)
        .Build();

        _mqttClient.ApplicationMessageReceivedAsync += e =>
        {
            Console.WriteLine($"Received application message from {e.ClientId}.");

            return Task.CompletedTask;
        };

        var mqttSubscribeOptions = mqttFactory
            .CreateSubscribeOptionsBuilder()
            .WithTopicFilter(f => f.WithTopic("telemetry_queue/+"))
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

        Console.WriteLine("MQTT client subscribed to topic.");
        Console.WriteLine(" Press [CTRL+C] to exit.");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("MessageProcessing Exited");
        return Task.CompletedTask;
    }

}
