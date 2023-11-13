using Business.Core.Auth.Commands;
using Business.Infrastructure.Security;
using Business.Repository;
using Domain.Data;
using MediatR;
using System.Security.Claims;
using System.Text;

namespace Business.Core.Auth.Handlers
{
    internal class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResult>
    {

        private readonly Business.Infrastructure.Security.IPasswordHasher _passHasher;
        private readonly UserRepository _userRepository;

        public LoginCommandHandler(IPasswordHasher passHasher, UserRepository userRepository)
        {
            _passHasher = passHasher;
            _userRepository = userRepository;
        }

        public async Task<LoginCommandResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            if (request.Email == null || request.Password == null)
            {
                return new LoginCommandResult() { IsSuccess = false, Message = "Bad email or password" };
            }

            var usr = await _userRepository.GetByEmailAsync(request.Email);

            if (usr == null)
            {
                // no user with this mail

                return new LoginCommandResult() { IsSuccess = false, Message = "Bad email or password" };
            }

            var isPwdCorrect = _passHasher.VerifyHashedPassword(usr.PasswordHash!, request.Password);

            if (!isPwdCorrect)
            {
                // wrong password

                return new LoginCommandResult() { IsSuccess = false, Message = "Bad email or password" };
            }

            return new LoginCommandResult { IsSuccess = true, User = usr };

        }
    }
}
