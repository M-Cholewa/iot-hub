// See https://aka.ms/new-console-template for more information
using Business.Core.Device.Commands;
using Business.Repository;
using Domain.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

// add users secrets
builder.Configuration.AddUserSecrets<Program>();

// add main function
builder.Services.AddHostedService<MQTTServer>();

// PostgreSQL
builder.Services.AddDbContext<IoTHubContext>(
option =>
{
    var pgconn = builder.Configuration.GetConnectionString("PostgreSQL");
    option.UseLazyLoadingProxies();
    option.UseNpgsql(pgconn);
});

// Repositories
var assemblies = Assembly
       .GetAssembly(typeof(BaseRepository<>))!
       .GetExportedTypes()
       .Where(t => !t.IsAbstract && t.GetInterfaces().Any(iface => iface == typeof(Business.Interface.IRepository)));

foreach (var assembly in assemblies)
{
    builder.Services.AddScoped(assembly);
}

// MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<ExecuteDirectMethodCommand>());

var app = builder.Build();

// * ======================= START APP ======================= *

app.Run();