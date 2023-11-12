using Business.Core.User.Commands;
using iot_hub_backend.Infrastructure.Security.AuthorizeAttribute;
using iot_hub_backend.Infrastructure.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iot_hub_backend.Controllers
{
    [Authorize]
    [HasRole(Roles.Admin)]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<RegisterUserCommandResult> RegisterUser([FromBody] RegisterUserCommand cmd)
        {
            return await _mediator.Send(cmd).ConfigureAwait(false);
        }

        [HttpPost("RemoveUser")]
        public async Task<RemoveUserCommandResult> RemoveUser([FromBody] RemoveUserCommand cmd)
        {
            return await _mediator.Send(cmd).ConfigureAwait(false);
        }
    }
}
