using Business.Core.User.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Core.User.Handlers
{
    public class RemoveUserCommandHandler : IRequestHandler<RemoveUserCommand, RemoveUserCommandResult>
    {
        public Task<RemoveUserCommandResult> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
