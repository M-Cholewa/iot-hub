using Business.Core.Device.Commands;
using iot_hub_backend.Infrastructure.Security.AuthorizeAttribute;
using iot_hub_backend.Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Domain.Core;
using Domain.Data;
using iot_hub_backend.Infrastructure.Extensions;

namespace iot_hub_backend.Controllers
{
    [Authorize]
    [HasRole(Roles.User)]
    [ApiController]
    public class DeviceController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly IoTHubContext _context;

        public DeviceController(IMediator mediator, IoTHubContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        [HttpPost("ExecuteDirectMethod")]
        public async Task<ExecuteDirectMethodCommandResult> ExecuteDirectMethod([FromBody] ExecuteDirectMethodCommand cmd)
        {
            return await _mediator.Send(cmd).ConfigureAwait(false);
        }

        [HttpPost("CreateDevice")]
        public async Task<CreateDeviceCommandResult> CreateDevice([FromBody] CreateDeviceCommand cmd)
        {
            return await _mediator.Send(cmd).ConfigureAwait(false);
        }

        [HttpPost("RemoveDevice")]
        public async Task<RemoveDeviceCommandResult> RemoveDevice([FromBody] RemoveDeviceCommand cmd)
        {
            return await _mediator.Send(cmd).ConfigureAwait(false);
        }

        [HttpPost("GetDevice")]
        public Device? GetDevice(Guid id)
        {
            return User.GetDevice(id, _context);
        }

        [HttpPost("GetDevices")]
        public List<Device>? GetDeviceList()
        {
            var user = User.GetUser(_context);

            if (user == null)
            {
                return null;
            }

            return user.Devices;
        }

    }
}
