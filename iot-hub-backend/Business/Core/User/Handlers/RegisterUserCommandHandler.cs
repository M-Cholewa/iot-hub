using Business.Core.User.Commands;
using Business.Infrastructure.Security;
using Domain.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Core.User.Handlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserCommandResult>
    {
        private readonly IoTHubContext _context;
        private readonly Business.Infrastructure.Security.IPasswordHasher _passHasher;

        public RegisterUserCommandHandler(IoTHubContext context, IPasswordHasher passHasher)
        {
            _context = context;
            _passHasher = passHasher;
        }

        public async Task<RegisterUserCommandResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {

            if(request.Password == null || request.Email == null)
            {
                return new RegisterUserCommandResult() { IsSuccess = false };
            }

            string pwdHash = _passHasher.HashPassword(request.Password);
            var user = new Domain.Core.User { Email = request.Email, PasswordHash = pwdHash };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return new RegisterUserCommandResult() { IsSuccess = true, ResultUser = user };
        }
    }
}
