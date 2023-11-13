using Business.Core.Device.Commands;
using Business.Infrastructure.Security;
using Business.Repository;
using Domain.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Core.Device.Handlers
{
    public class CreateDeviceCommandHandler : IRequestHandler<CreateDeviceCommand, CreateDeviceCommandResult>
    {
        private readonly DeviceRepository _deviceRepository;
        private readonly MQTTUserRepository _mqttUserRepository;
        private readonly Business.Infrastructure.Security.IPasswordHasher _passHasher;

        public CreateDeviceCommandHandler(DeviceRepository deviceRepository, MQTTUserRepository mqttUserRepository, IPasswordHasher passHasher)
        {
            _deviceRepository = deviceRepository;
            _mqttUserRepository = mqttUserRepository;
            _passHasher = passHasher;
        }

        public async Task<CreateDeviceCommandResult> Handle(CreateDeviceCommand request, CancellationToken cancellationToken)
        {
            if (request.Owner == null || request.Device == null || request.MqttPassword == null || request.MqttUsername == null)
            {
                return new CreateDeviceCommandResult() { IsSuccess = false, Message = "Bad request" };
            }

            var _credentials = new[] { request.MqttPassword, request.MqttUsername };

            if (_credentials.Any(x => x.Contains(';') || x.Contains('=')))
            {
                return new CreateDeviceCommandResult() { IsSuccess = false, Message = "MQTT username or password contains invalid characters" };
            }


            if (_credentials.Any(x => x.Length < 8))
            {
                return new CreateDeviceCommandResult() { IsSuccess = false, Message = "MQTT username or password is too short" };
            };

            var mqttUserPasswordHash = _passHasher.HashPassword(request.MqttPassword);
            var mqttUser = await _mqttUserRepository.AddAsync(new Domain.Core.MQTTUser { Username = request.MqttUsername, PasswordHash = mqttUserPasswordHash });

            if (mqttUser == null)
            {
                return new CreateDeviceCommandResult() { IsSuccess = false, Message = "Failed to create MQTT user" };
            }

            var device = new Domain.Core.Device { Name = request.Device.Name, Owner = request.Owner, MQTTUser = mqttUser };

            device = await _deviceRepository.AddAsync(device);

            var _apiKey = $"ClientID={device.MQTTUser!.ClientID};User=${request.MqttUsername};Pass=${request.MqttPassword}";

            return new CreateDeviceCommandResult() { IsSuccess = true, ResultDevice = device, MqttApiKey = _apiKey };
        }
    }
}
