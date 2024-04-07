using Business.Core.User.Commands;
using Business.Infrastructure.Security;
using Business.Repository;
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
        private readonly UserRepository _userRepository;
        private readonly Business.Infrastructure.Security.IPasswordHasher _passHasher;
        private readonly RoleRepository _roleRepository;

        public RegisterUserCommandHandler(UserRepository userRepository, IPasswordHasher passHasher, RoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _passHasher = passHasher;
            _roleRepository = roleRepository;
        }

        public async Task<RegisterUserCommandResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {

            if (request.Password == null || request.Email == null)
            {
                return new RegisterUserCommandResult() { IsSuccess = false, Message = "Bad email or password" };
            }

            string pwdHash = _passHasher.HashPassword(request.Password);
            var user = new Domain.Core.User { Email = request.Email, PasswordHash = pwdHash };

            user = await _userRepository.AddAsync(user);
            var userRole = await _roleRepository.GetByKeyAsync(Domain.Core.Role.User).ConfigureAwait(false);

            if(userRole == null)
            {
                return new RegisterUserCommandResult() { IsSuccess = false, Message = "User role not found" };
            }

            await _userRepository.GrantRoleAsync(user, userRole).ConfigureAwait(false);

            return new RegisterUserCommandResult() { IsSuccess = true, ResultUser = user };
        }
    }
}
