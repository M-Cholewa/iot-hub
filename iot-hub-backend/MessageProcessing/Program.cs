﻿// See https://aka.ms/new-console-template for more information
using Business.Core.Device.Commands;
using Business.InfluxRepository;
using Business.Repository;
using Domain.Data;
using MessageProcessing.Messages.Requests;
using MessageProcessing.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

// add users secrets
builder.Configuration.AddUserSecrets<Program>();

// configure MQTT
var mqttConnectionConfig = builder.Configuration.GetSection("MQTTConnectionConfig").Get<Communication.MQTT.Config.MQTTConnectionConfig>();
builder.Services.AddSingleton(mqttConnectionConfig!);

// add main function
builder.Services.AddHostedService<InterceptingService>();
builder.Services.AddHostedService<GeneralTelemetryService>();

// PostgreSQL
builder.Services.AddDbContext<IoTHubContext>(
option =>
{
    var pgconn = builder.Configuration.GetConnectionString("PostgreSQL");
    option.UseLazyLoadingProxies();
    option.UseNpgsql(pgconn);
}, ServiceLifetime.Transient);

// Repositories
var assemblies = Assembly
       .GetAssembly(typeof(BaseRepository<>))!
       .GetExportedTypes()
       .Where(t => !t.IsAbstract && t.GetInterfaces().Any(iface => iface == typeof(Business.Interface.IRepository)));

foreach (var assembly in assemblies)
{
    builder.Services.AddScoped(assembly);
}

// Influx repositories
var influxRepositoryConnection = builder.Configuration.GetSection("InfluxRepositoryConnection").Get<InfluxRepositoryConnection>();
builder.Services.AddSingleton(influxRepositoryConnection!);

var influxAssemblies = Assembly
       .GetAssembly(typeof(TelemetryRepository))!
       .GetExportedTypes()
       .Where(t => !t.IsAbstract && t.GetInterfaces().Any(iface => iface == typeof(Business.Interface.IInfluxRepository)));


foreach (var assembly in influxAssemblies)
{
    builder.Services.AddTransient(assembly);
}

// MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<AddLogCommand>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<ExecuteDirectMethodCommand>());

var app = builder.Build();

// * ======================= START APP ======================= *

app.Run();