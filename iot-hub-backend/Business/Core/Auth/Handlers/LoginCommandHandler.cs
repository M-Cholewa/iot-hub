using Business.Core.Auth.Commands;
using Business.Infrastructure.Security;
using Domain.Data;
using MediatR;
using System.Security.Claims;
using System.Text;

namespace Business.Core.Auth.Handlers
{
    internal class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResult>
    {

        private readonly Business.Infrastructure.Security.IPasswordHasher _passHasher;
        private readonly IoTHubContext _context;

        public LoginCommandHandler(IPasswordHasher passHasher, IoTHubContext context)
        {
            _passHasher = passHasher;
            _context = context;
        }

        public async Task<LoginCommandResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            if (request.Email == null || request.Password == null)
            {
                return new LoginCommandResult() { IsSuccess = false };
            }

            var usr = _context.Users.Where(x => x.Email == request.Email).First();

            if (usr == null)
            {
                // no user with this mail

                return new LoginCommandResult() { IsSuccess = false };
            }

            var isPwdCorrect = _passHasher.VerifyHashedPassword(usr.PasswordHash!, request.Password);

            if (!isPwdCorrect)
            {
                // wrong password

                return new LoginCommandResult() { IsSuccess = false };
            }

            return new LoginCommandResult { IsSuccess = true, User = usr };

        }
    }
}
