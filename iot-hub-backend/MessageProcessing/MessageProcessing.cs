using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communication.DeviceConnection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public class MessageProcessing : IHostedService, IDisposable
{

    private ManualResetEvent _exitEvent;
    private IConnection _connection;
    private IModel _channel;

    public MessageProcessing(RabbitMQConnectionConfig rabbitMQConnection)
    {
        var factory = new ConnectionFactory
        {
            Uri = new Uri(rabbitMQConnection.UriString)
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _exitEvent = new ManualResetEvent(false);

        Console.CancelKeyPress += (sender, eArgs) =>
        {
            _exitEvent.Set();
        };
    }

    /// <summary>
    /// Main program function
    /// </summary>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _channel.QueueDeclare(queue: "telemetry_queue",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

        Console.WriteLine(" [*] Waiting for messages.");

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($" [x] Received {message}");
        };

        _channel.BasicConsume(queue: "telemetry_queue",
                             autoAck: true,
                             consumer: consumer);

        Console.WriteLine(" Press [CTRL+C] to exit.");

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _channel.Close();
        _connection.Close();
    }
}
