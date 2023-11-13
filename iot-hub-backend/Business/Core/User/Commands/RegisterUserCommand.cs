using Domain.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Core.User.Commands
{
    public class RegisterUserCommandResult
    {
        public bool IsSuccess { get; set; } = false;
        public Domain.Core.User? ResultUser { get; set; }
        public string Message { get; set; } = "";
    }

    public class RegisterUserCommand : IRequest<RegisterUserCommandResult>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public List<Role>? Roles { get; set; }

        public RegisterUserCommand()
        {
        }
    }
}
