using Business.Core.User.Commands;
using Business.Repository;
using Domain.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using iot_hub_backend.Infrastructure.Security.AuthorizeAttribute;

namespace iot_hub_backend.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public partial class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserRepository _userRepository;

        public UserController(IMediator mediator, UserRepository userRepository)
        {
            _mediator = mediator;
            _userRepository = userRepository;
        }

        [HttpPut]
        public async Task<RegisterUserCommandResult> RegisterUser([FromBody] RegisterUserCommand cmd)
        {
            return await _mediator.Send(cmd).ConfigureAwait(false);
        }

        [HttpDelete]
        [HasRole(Role.User)]
        public async Task<RemoveUserCommandResult> RemoveUser([FromBody] RemoveUserCommand cmd)
        {
            return await _mediator.Send(cmd).ConfigureAwait(false);
        }

        [HttpGet("All")]
        [HasRole(Role.Admin)]
        public async Task<IList<User>?> GetAllUsers()
        {
            return await _userRepository.GetAllAsync().ConfigureAwait(false);
        }
    }
}
