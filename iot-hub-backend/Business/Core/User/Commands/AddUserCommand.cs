using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Core.User.Commands
{
    public class AddUserCommandResult
    {
        public bool IsSuccess { get; set; } = false;
        public Domain.Core.User? ResultUser { get; set; }
    }

    public class AddUserCommand : IRequest<AddUserCommandResult>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }

        public AddUserCommand()
        {
        }
    }
}
