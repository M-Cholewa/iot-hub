using Business.Core.Device.Commands;
using iot_hub_backend.Infrastructure.Security.AuthorizeAttribute;
using iot_hub_backend.Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Domain.Core;
using Domain.Data;
using iot_hub_backend.Infrastructure.Extensions;
using Common;
using Business.Repository;
using Business.InfluxRepository;

namespace iot_hub_backend.Controllers
{
    [Authorize]
    [HasRole(Role.User)]
    [ApiController]
    public class DeviceController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly UserRepository _userRepository;
        private readonly TelemetryRepository _telemetryRepository;
        private readonly LogRepository _logRepository;

        public DeviceController(IMediator mediator, UserRepository userRepository, TelemetryRepository telemetryRepository, LogRepository logRepository)
        {
            _mediator = mediator;
            _userRepository = userRepository;
            _telemetryRepository = telemetryRepository;
            _logRepository = logRepository;
        }

        [HttpPost("ExecuteDirectMethod")]
        public async Task<ExecuteDirectMethodCommandResult> ExecuteDirectMethod([FromBody] ExecuteDirectMethodCommand cmd)
        {
            return await _mediator.Send(cmd).ConfigureAwait(false);
        }


        [HttpPost("CreateDevice")]
        public async Task<CreateDeviceCommandResult> CreateDevice(string deviceName)
        {

            var ownerId = User.GetGuid();
            var mqttUsername = Password.Generate(30, 2);
            var mqttPassword = Password.Generate(60, 4);
            var device = new Device { Name = deviceName };

            var cmd = new CreateDeviceCommand { Device = device, MqttPassword = mqttPassword, MqttUsername = mqttUsername, OwnerId = ownerId };

            return await _mediator.Send(cmd).ConfigureAwait(false);
        }


        [HttpPost("RemoveDevice")]
        public async Task<RemoveDeviceCommandResult> RemoveDevice([FromBody] RemoveDeviceCommand cmd)
        {
            return await _mediator.Send(cmd).ConfigureAwait(false);
        }


        [HttpGet("GetDevice")]
        public async Task<Device?> GetDevice(Guid id)
        {
            return await User.GetDevice(id, _userRepository);
        }

        [HttpGet("GetDevices")]
        public async Task<List<Device>?> GetDeviceList()
        {
            var user = await User.GetUser(_userRepository);

            if (user == null)
            {
                return null;
            }

            return user.Devices?.ToList();
        }

        [HttpGet("GetLogs")]
        public List<Log> GetAllLogs(Guid deviceId)
        {
            return _logRepository.GetAll(deviceId);
        }

        [HttpGet("GetLastLogs")]
        public List<Log> GetLastLogs(Guid deviceId, int limit)
        {
            return _logRepository.GetAll(deviceId, limit);
        }

        [HttpGet("GetTelemetry")]
        public List<Telemetry> GetTelemetries(Guid deviceId, string fieldName, DateTime sinceUTC, DateTime toUTC)
        {
            return _telemetryRepository.Get(deviceId, fieldName, sinceUTC, toUTC);
        }


        [HttpGet("GetTelemetryFieldNames")]
        public async Task<List<string>> GetTelemetryFieldNames(Guid deviceId)
        {
            return await _telemetryRepository.GetFieldNames(deviceId);
        }


    }
}
