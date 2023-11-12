using Business.Core.Device.Commands;
using iot_hub_backend.Infrastructure.Security.AuthorizeAttribute;
using iot_hub_backend.Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Domain.Core;
using Domain.Data;

namespace iot_hub_backend.Controllers
{
    [Authorize]
    [HasRole(Roles.User)]
    [ApiController]
    public class DeviceController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly IoTHubContext _context;


        [HttpPost("ExecuteDirectMethod")]
        public async Task<ExecuteDirectMethodCommandResult> ExecuteDirectMethod([FromBody] ExecuteDirectMethodCommand cmd)
        {
            return await _mediator.Send(cmd).ConfigureAwait(false);
        }

        [HttpPost("CreateDevice")]
        public async Task<CreateDeviceComandResult> CreateDeviceComand([FromBody] CreateDeviceCommand cmd)
        {
            return await _mediator.Send(cmd).ConfigureAwait(false);
        }

        [HttpPost("RemoveDevice")]
        public async Task<RemoveDeviceCommandResult> RemoveDevice([FromBody] RemoveDeviceCommand cmd)
        {
            return await _mediator.Send(cmd).ConfigureAwait(false);
        }

        [HttpPost("GetDevice")]
        public async Task<Device> GetDevice(Guid id)
        {
            return await _mediator.Send(cmd).ConfigureAwait(false);
        }

        [HttpPost("GetDevices")]
        public async Task<List<Device>> GetDevices()
        {
            return await _mediator.Send(cmd).ConfigureAwait(false);
        }

    }
}
