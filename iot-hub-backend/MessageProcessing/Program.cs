// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

// add users secrets
builder.Configuration.AddUserSecrets<Program>();

// configure MQTT
var mqttConnectionConfig = builder.Configuration.GetSection("MQTTConnectionConfig").Get<Communication.MQTT.Config.MQTTConnectionConfig>();
builder.Services.AddSingleton(mqttConnectionConfig!);

// add main function
builder.Services.AddHostedService<MessageProcessing>();

var app = builder.Build();

// * ======================= START APP ======================= *

app.Run();