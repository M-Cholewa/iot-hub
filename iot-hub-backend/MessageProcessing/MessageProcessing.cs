using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communication.DeviceConnection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public class MessageProcessing
{

    private readonly RabbitMQConnectionConfig _rabbitMQConnection;

    public MessageProcessing(RabbitMQConnectionConfig rabbitMQConnection)
    {
        _rabbitMQConnection = rabbitMQConnection;
    }



    /// <summary>
    /// Main program function
    /// </summary>
    public void Run()
    {
        var factory = new RabbitMQ.Client.ConnectionFactory
        {
            Uri = new Uri(_rabbitMQConnection.UriString)
        };
        
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "hello",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        Console.WriteLine(" [*] Waiting for messages.");

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($" [x] Received {message}");
        };
        channel.BasicConsume(queue: "hello",
                             autoAck: true,
                             consumer: consumer);

        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();
    }
}
