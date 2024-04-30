using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageProcessing;
using Business.InfluxRepository;
using Business.Interface;
using Business.Repository;
using Communication.MQTT.Config;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SystemTest.Base
{
    public class MessageProcessingFactory
    {

        public async Task Run()
        {
            await using HostApplicationFactory<Program> hostApplicationFactory =
                new(configuration: builder =>
                {
                    //// Replace any appsettings like dynamic ports from test containers
                    //builder.UseSetting("SomeAppSetting:Key", "replacement value");

                    builder.ConfigureTestServices(services =>
                    {

                        // not used but used

                        services.AddSingleton<Communication.MQTT.IRpcClient, Communication.MQTT.RpcClient>();
                        services.AddTransient<IUserRepository, UserRepository>();




                        services.AddTransient<Business.Infrastructure.Security.IPasswordHasher, Business.Infrastructure.Security.PasswordHasher>();

                    });
                });

            await hostApplicationFactory.RunHostAsync();

        }
    }
}
