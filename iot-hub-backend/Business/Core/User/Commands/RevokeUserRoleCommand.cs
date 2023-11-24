using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Core.User.Commands
{
    public class RevokeUserRoleCommandResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = "";
    }

    public class RevokeUserRoleCommand : IRequest<RevokeUserRoleCommandResult>
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
