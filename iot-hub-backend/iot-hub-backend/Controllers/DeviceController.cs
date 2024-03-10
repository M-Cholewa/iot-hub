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
using Domain.InfluxDB;

namespace iot_hub_backend.Controllers
{
    [Authorize]
    [HasRole(Role.User)]
    [Route("[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly UserRepository _userRepository;
        private readonly ConsoleRecordRepository _recordRepository;

        public DeviceController(IMediator mediator, UserRepository userRepository, ConsoleRecordRepository recordRepository)
        {
            _mediator = mediator;
            _userRepository = userRepository;
            _recordRepository = recordRepository;
        }

        [HttpPost("ExecuteDirectMethod")]
        public async Task<ExecuteDirectMethodCommandResult> ExecuteDirectMethod([FromBody] ExecuteDirectMethodCommand cmd)
        {
            return await _mediator.Send(cmd).ConfigureAwait(false);
        }

        [HttpGet("AllConsoleRecords")]
        public List<ConsoleRecord> GetAllConsoleRecords(Guid deviceId)
        {
            return _recordRepository.GetAll(deviceId);
        }

        [HttpPut]
        public async Task<CreateDeviceCommandResult> CreateDevice(string deviceName)
        {

            var ownerId = User.GetGuid();
            var mqttUsername = Password.Generate(30, 2);
            var mqttPassword = Password.Generate(60, 4);
            var device = new Device { Name = deviceName };

            var cmd = new CreateDeviceCommand { Device = device, MqttPassword = mqttPassword, MqttUsername = mqttUsername, OwnerId = ownerId };

            return await _mediator.Send(cmd).ConfigureAwait(false);
        }


        [HttpDelete]
        public async Task<RemoveDeviceCommandResult> RemoveDevice([FromBody] RemoveDeviceCommand cmd)
        {
            return await _mediator.Send(cmd).ConfigureAwait(false);
        }


        [HttpGet]
        public async Task<Device?> GetDevice(Guid id)
        {
            return await User.GetDevice(id, _userRepository);
        }


        [HttpGet("ThisUser")]
        public async Task<List<Device>?> GetDeviceList()
        {
            var user = await User.GetUser(_userRepository);

            if (user == null)
            {
                return null;
            }

            return user.Devices?.ToList();
        }

    }
}
