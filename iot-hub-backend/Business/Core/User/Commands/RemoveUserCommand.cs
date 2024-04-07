using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Core.User.Commands
{
    public class RemoveUserCommandResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = "";
    }

    public class RemoveUserCommand : IRequest<RemoveUserCommandResult>
    {
        public Guid UserId { get; set; }
    }
}
