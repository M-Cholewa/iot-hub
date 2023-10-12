// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

// add users secrets
builder.Configuration.AddUserSecrets<Program>();

// configure rabbitmq
var rabbitMQConnectionConfig = builder.Configuration.GetSection("RabbitMQConnectionConfig").Get<Communication.DeviceConnection.RabbitMQConnectionConfig>();
builder.Services.AddSingleton(rabbitMQConnectionConfig!);


// * ======================= START APP ======================= *
var mp = new MessageProcessing(rabbitMQConnectionConfig!);
mp.Run();