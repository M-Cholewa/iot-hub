using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Core.Auth.Commands
{
    public class LoginCommandResult
    {
        public bool IsSuccess { get; set; }
        public Domain.Core.User? User { get; set; }
    }

    public class LoginCommand : IRequest<LoginCommandResult>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
