using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Core.User.Commands
{
    public class GrantUserRoleCommandResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = "";
    }

    public class GrantUserRoleCommand : IRequest<GrantUserRoleCommandResult>
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
