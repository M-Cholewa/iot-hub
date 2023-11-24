using Business.Core.User.Commands;
using iot_hub_backend.Infrastructure.Security.AuthorizeAttribute;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Business.Repository;
using Domain.Core;

namespace iot_hub_backend.Controllers
{
    [Authorize]
    [HasRole(Role.Admin)]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserRepository _userRepository;

        public AdminController(IMediator mediator, UserRepository userRepository)
        {
            _mediator = mediator;
            _userRepository = userRepository;
        }

        [HttpPost("RemoveUser")]
        public async Task<RemoveUserCommandResult> RemoveUser([FromBody] RemoveUserCommand cmd)
        {
            return await _mediator.Send(cmd).ConfigureAwait(false);
        }

        [HttpPost("GetAllUsers")]
        public async Task<IList<User>?> GetAllUsers()
        {
            return await _userRepository.GetAllAsync().ConfigureAwait(false);
        }


        [HttpPost("GrantUserRole")]
        public async Task<GrantUserRoleCommandResult> GrantUserRole([FromBody] GrantUserRoleCommand cmd)
        {
            return await _mediator.Send(cmd).ConfigureAwait(false);
        }

        [HttpPost("RevokeUserRole")]
        public async Task<RevokeUserRoleCommandResult> RemoveUserRole([FromBody] RevokeUserRoleCommand cmd)
        {
            return await _mediator.Send(cmd).ConfigureAwait(false);
        }
    }
}
