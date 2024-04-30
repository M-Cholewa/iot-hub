//#define CREATE_DB

using Business.Core.Device.Commands;
using Business.InfluxRepository;
using Business.Interface;
using Business.Repository;
using Castle.DynamicProxy.Internal;
using Domain.Core;
using Domain.Data;
using InfluxDB.Client;
using iot_hub_backend.Infrastructure.Security;
using iot_hub_backend.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text;


namespace iot_hub_backend
{
    class Program
    {
        static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
            builder.Services.AddSingleton(jwtSettings);

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key!)),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true
                    };
                });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.UserPolicyName, p =>
                    p.RequireClaim(Role.User, "true"));
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "My Good API",
                    Version = "v1",
                    Description = "Doesn't hurt to add some description."
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

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
                   .Where(t => t.BaseType != null && t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == typeof(BaseRepository<>));


            foreach (var assembly in assemblies)
            {
                builder.Services.AddScoped(assembly);
            }

            builder.Services.AddScoped<IUserRepository, UserRepository>();

            // Influx repositories
            var influxRepositoryConnection = builder.Configuration.GetSection("InfluxRepositoryConnection").Get<InfluxRepositoryConnection>();
            builder.Services.AddSingleton(influxRepositoryConnection);

            var influxAssemblies = Assembly
                   .GetAssembly(typeof(TelemetryRepository))!
                   .GetExportedTypes()
                   .Where(t => !t.IsAbstract && t.GetInterfaces().Any(iface => iface == typeof(Business.Interface.IInfluxRepository)));


            foreach (var assembly in influxAssemblies)
            {
                builder.Services.AddScoped(assembly);
            }


            // For user password
            builder.Services.AddScoped<Business.Infrastructure.Security.IPasswordHasher, Business.Infrastructure.Security.PasswordHasher>();

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<ExecuteDirectMethodCommand>());

            // configure MQTT
            var mqttConnectionConfig = builder.Configuration.GetSection("MQTTConnectionConfig").Get<Communication.MQTT.Config.MQTTConnectionConfig>();
            builder.Services.AddSingleton(mqttConnectionConfig);
            builder.Services.AddSingleton<Communication.MQTT.IRpcClient, Communication.MQTT.RpcClient>();

            // CORS
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy.WithOrigins("https://localhost:3000", "http://localhost:3000") // REACT FRONT
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                                .AllowCredentials();
                    });
            });


            var app = builder.Build();

#if CREATE_DB

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<IoTHubContext>();
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
}

#endif

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}