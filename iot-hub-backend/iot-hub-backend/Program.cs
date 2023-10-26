//#define CREATE_DB

using iot_hub_backend.Data;
using iot_hub_backend.Infrastructure.Security;
using iot_hub_backend.Swagger;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

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
    options.AddPolicy(IdentityData.AdminUserPolicyName, p =>
        p.RequireClaim(IdentityData.AdminUserClaimName, "true"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

// PostgreSQL
builder.Services.AddDbContext<IoTHubContext>(
option =>
{
    var pgconn = builder.Configuration.GetConnectionString("PostgreSQL");
    option.UseNpgsql(pgconn);
});

// For user password
builder.Services.AddScoped<Business.Infrastructure.Security.IPasswordHasher, Business.Infrastructure.Security.PasswordHasher>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Business.Core.Device.Commands.ExecuteDirectMethodCommand>());

var rabbitMQConnectionConfig = builder.Configuration.GetSection("RabbitMQConnectionConfig").Get<Communication.DeviceConnection.RabbitMQConnectionConfig>();
builder.Services.AddSingleton(rabbitMQConnectionConfig);
builder.Services.AddSingleton<Communication.DeviceConnection.IRpcClient, Communication.DeviceConnection.RpcClient>();

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
