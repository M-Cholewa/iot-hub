using Business.Core.User.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace iot_hub_backend.Controllers
{
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand cmd)
        {
            var _register = await _mediator.Send(cmd).ConfigureAwait(false);

            if (!_register.IsSuccess)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            return Ok();
        }

        [HttpPost("RemoveUser")]
        public async Task<IActionResult> RemoveUser([FromBody] RemoveUserCommand cmd)
        {
            var _remove = await _mediator.Send(cmd).ConfigureAwait(false);

            if (!_remove.IsSuccess)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            return Ok();
        }
    }
}
