using Business.Core.User.Commands;
using iot_hub_backend.Infrastructure.Security.AuthorizeAttribute;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Domain.Core;
using Microsoft.AspNetCore.Authorization;

namespace iot_hub_backend.Controllers
{
    [Authorize]
    [HasRole(Role.Admin)]
    [Route("[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserRoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GrantUserRole")]
        public async Task<GrantUserRoleCommandResult> GrantUserRole([FromBody] GrantUserRoleCommand cmd)
        {
            return await _mediator.Send(cmd).ConfigureAwait(false);
        }

        [HttpPost("RevokeUserRole")]
        public async Task<RevokeUserRoleCommandResult> RevokeUserRole([FromBody] RevokeUserRoleCommand cmd)
        {
            return await _mediator.Send(cmd).ConfigureAwait(false);
        }
    }
}
