using MediatR;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors();

app.MapControllers();

app.Run();
