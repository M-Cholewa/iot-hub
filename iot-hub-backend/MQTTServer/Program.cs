﻿// See https://aka.ms/new-console-template for more information
using Business.Core.Device.Commands;
using Business.InfluxRepository;
using Business.Repository;
using Castle.DynamicProxy.Internal;
using Communication.MQTT.Config;
using Domain.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace MQTTServer {
    class Program
    {
        static void Main(string[] args)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

            // add users secrets
            builder.Configuration.AddUserSecrets<Program>();

            // add main function
            builder.Services.AddHostedService<MQTTServer.Services.ServerService>();

            // PostgreSQL
            builder.Services.AddDbContext<IoTHubContext>(
            option =>
            {
                var pgconn = builder.Configuration.GetConnectionString("PostgreSQL");
                option.UseLazyLoadingProxies();
                option.UseNpgsql(pgconn);
            }, contextLifetime: ServiceLifetime.Transient,
                optionsLifetime: ServiceLifetime.Transient);

            // Repositories
            var assemblies = Assembly
                   .GetAssembly(typeof(BaseRepository<>))!
                   .GetExportedTypes()
                   .Where(t => t.BaseType != null && t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == typeof(BaseRepository<>));

            foreach (var assembly in assemblies)
            {
                builder.Services.AddTransient(assembly);
            }

            // For user password
            builder.Services.AddScoped<Business.Infrastructure.Security.IPasswordHasher, Business.Infrastructure.Security.PasswordHasher>();

            // MediatR
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<ExecuteDirectMethodCommand>());



            var app = builder.Build();

            // * ======================= START APP ======================= *

            app.Run();
        }
    }
}
