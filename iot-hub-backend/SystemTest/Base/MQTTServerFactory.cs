using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MQTTServer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.TestHost;
using Business.InfluxRepository;
using Communication.MQTT.Config;
using System.Reflection;
using Business.Interface;
using Business.Repository;

namespace SystemTest.Base
{
    public class MQTTServerFactory
    {
        public MQTTServerFactory()
        {

        }

        public async Task Run()
        {
            await using HostApplicationFactory<Program> hostApplicationFactory =
                new(configuration: builder =>
                {
                    // Replace any appsettings like dynamic ports from test containers
                    //builder.UseSetting("SomeAppSetting:Key", "replacement value");

                    builder.ConfigureTestServices(services =>
                    {

                        // not used but used
                        var mqttConnectionConfig = new MQTTConnectionConfig();
                        services.AddSingleton(mqttConnectionConfig!);
                        services.AddSingleton<Communication.MQTT.IRpcClient, Communication.MQTT.RpcClient>();
                        var influxRepositoryConnection = new InfluxRepositoryConnection();
                        services.AddSingleton(influxRepositoryConnection);

                        services.AddScoped<IUserRepository, UserRepository>();


                        var influxAssemblies = Assembly
                           .GetAssembly(typeof(TelemetryRepository))!
                           .GetExportedTypes()
                           .Where(t => !t.IsAbstract && t.GetInterfaces().Any(iface => iface == typeof(IInfluxRepository)));


                        foreach (var assembly in influxAssemblies)
                        {
                            services.AddScoped(assembly);
                        }

                        services.AddTransient<Business.Infrastructure.Security.IPasswordHasher, Business.Infrastructure.Security.PasswordHasher>();

                    });


                });

            await hostApplicationFactory.RunHostAsync();

        }
    }
}
