using Business.Core.Device.Commands;
using Business.Infrastructure.Security;
using Business.Repository;
using Communication.MQTT.Config;
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
        private readonly UserRepository _userRepository;
        private readonly Business.Infrastructure.Security.IPasswordHasher _passHasher;
        private readonly MQTTConnectionConfig _mqttConnectionConfig;

        public CreateDeviceCommandHandler(DeviceRepository deviceRepository, MQTTUserRepository mqttUserRepository, UserRepository userRepository, IPasswordHasher passHasher, MQTTConnectionConfig mqttConnectionConfig)
        {
            _deviceRepository = deviceRepository;
            _mqttUserRepository = mqttUserRepository;
            _userRepository = userRepository;
            _passHasher = passHasher;
            _mqttConnectionConfig = mqttConnectionConfig;
        }

        public async Task<CreateDeviceCommandResult> Handle(CreateDeviceCommand request, CancellationToken cancellationToken)
        {
            if (request.Device == null || request.MqttPassword == null || request.MqttUsername == null)
            {
                return new CreateDeviceCommandResult() { IsSuccess = false, Message = "Bad request" };
            }

            var _owner = await _userRepository.GetByIdAsync(request.OwnerId);

            if (_owner == null)
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
            var mqttUser = new Domain.Core.MQTTUser { Username = request.MqttUsername, PasswordHash = mqttUserPasswordHash }; //await _mqttUserRepository.AddAsync();

            //if (mqttUser == null)
            //{
            //    return new CreateDeviceCommandResult() { IsSuccess = false, Message = "Failed to create MQTT user" };
            //}

            var device = new Domain.Core.Device { Name = request.Device.Name, MQTTUser = mqttUser };

            device = await _deviceRepository.AddAsync(device);

            try
            {
                await _userRepository.AddDeviceAsync(_owner, device);
            }
            catch
            {
                return new CreateDeviceCommandResult() { IsSuccess = false, Message = "Failed to add device to user" };
            }

            var _apiKey = $"ClientID={device.MQTTUser!.ClientID};User={request.MqttUsername};Pass={request.MqttPassword};Server={_mqttConnectionConfig.ServerAddress}"; // in here the server address is constant but could be also round robin DNS or something else

            return new CreateDeviceCommandResult() { IsSuccess = true, ResultDevice = device, MqttApiKey = _apiKey };
        }
    }
}
