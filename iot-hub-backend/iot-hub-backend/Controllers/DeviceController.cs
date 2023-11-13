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

namespace iot_hub_backend.Controllers
{
    [Authorize]
    [HasRole(Roles.User)]
    [ApiController]
    public class DeviceController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly UserRepository _userRepository;

        public DeviceController(IMediator mediator, UserRepository userRepository)
        {
            _mediator = mediator;
            _userRepository = userRepository;
        }

        [HttpPost("ExecuteDirectMethod")]
        public async Task<ExecuteDirectMethodCommandResult> ExecuteDirectMethod([FromBody] ExecuteDirectMethodCommand cmd)
        {
            return await _mediator.Send(cmd).ConfigureAwait(false);
        }


        [HttpPost("CreateDevice")]
        public async Task<CreateDeviceCommandResult> CreateDevice(string deviceName)
        {

            var ownerId =  User.GetGuid();
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


        [HttpPost("GetDevice")]
        public async Task<Device?> GetDevice(Guid id)
        {
            return await User.GetDevice(id, _userRepository);
        }

        [HttpPost("GetDevices")]
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
