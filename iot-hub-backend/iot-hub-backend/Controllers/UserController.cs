using Business.Core.User.Commands;
using Business.Repository;
using Domain.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using iot_hub_backend.Infrastructure.Security.AuthorizeAttribute;
using iot_hub_backend.Infrastructure.Extensions;

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
            bool _canRemove = await User.IsAdminOrCertainUser(cmd.Id, _userRepository);

            if (!_canRemove)
            {
                return new RemoveUserCommandResult { IsSuccess = false, Message = "Unauthorized" };
            }

            return await _mediator.Send(cmd).ConfigureAwait(false);
        }

        [HttpPatch("Email")]
        [HasRole(Role.User)]
        public async Task<UpdateUserEmailCommandResult> UpdateUserEmail([FromBody] UpdateUserEmailCommand cmd)
        {
            bool _canUpdate = await User.IsAdminOrCertainUser(cmd.Id, _userRepository);

            if (!_canUpdate)
            {
                return new UpdateUserEmailCommandResult { IsSuccess = false, Message = "Unauthorized" };
            }

            return await _mediator.Send(cmd).ConfigureAwait(false);
        }

        [HttpPatch("Password")]
        [HasRole(Role.User)]
        public async Task<UpdateUserPasswordCommandResult> UpdateUserPassword([FromBody] UpdateUserPasswordCommand cmd)
        {
            bool _canUpdate = await User.IsAdminOrCertainUser(cmd.Id, _userRepository);

            if (!_canUpdate)
            {
                return new UpdateUserPasswordCommandResult { IsSuccess = false, Message = "Unauthorized" };
            }

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
