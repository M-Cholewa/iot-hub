using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemTest.Base
{
    public class HostApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint>
        where TEntryPoint : class
    {
        private readonly Action<IWebHostBuilder> _configuration;

        public HostApplicationFactory(Action<IWebHostBuilder> configuration)
        {
            _configuration = configuration;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            _configuration(builder.Configure(_ => { }));
        }

        public Task RunHostAsync()
        {
            var host = Services.GetRequiredService<IHost>();
            return host.WaitForShutdownAsync();
        }
    }
}
