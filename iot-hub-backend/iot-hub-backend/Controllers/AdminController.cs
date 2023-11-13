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
        private readonly RoleRepository _roleRepository;

        public AdminController(IMediator mediator, RoleRepository roleRepository)
        {
            _mediator = mediator;
            _roleRepository = roleRepository;
        }

        [HttpPost("RemoveUser")]
        public async Task<RemoveUserCommandResult> RemoveUser([FromBody] RemoveUserCommand cmd)
        {
            return await _mediator.Send(cmd).ConfigureAwait(false);
        }

        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole(string roleKey)
        {
            var role = new Domain.Core.Role { Key = roleKey };
            await _roleRepository.AddAsync(role).ConfigureAwait(false);
            return Ok();
        }
    }
}
