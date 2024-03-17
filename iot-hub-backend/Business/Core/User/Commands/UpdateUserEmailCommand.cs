using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Core.User.Commands
{
    public class UpdateUserEmailCommandResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = "";
    }

    public class UpdateUserEmailCommand : IRequest<UpdateUserEmailCommandResult>
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = "";
    }
}
