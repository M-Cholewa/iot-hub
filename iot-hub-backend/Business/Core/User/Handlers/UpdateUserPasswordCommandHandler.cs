using Business.Core.User.Commands;
using Business.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Core.User.Handlers
{
    public class UpdateUserPasswordCommandHandler : IRequestHandler<UpdateUserPasswordCommand, UpdateUserPasswordCommandResult>
    {
        private readonly UserRepository _userRepository;
        private readonly Business.Infrastructure.Security.IPasswordHasher _passHasher;

        public UpdateUserPasswordCommandHandler(UserRepository userRepository, Business.Infrastructure.Security.IPasswordHasher passHasher)
        {
            _userRepository = userRepository;
            _passHasher = passHasher;
        }

        public async Task<UpdateUserPasswordCommandResult> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var _user = await _userRepository.GetByIdAsync(request.UserId);

            if (_user == null)
            {
                return new UpdateUserPasswordCommandResult() { IsSuccess = false, Message = "User does not exist" };
            }

            var isPwdCorrect = _passHasher.VerifyHashedPassword(_user.PasswordHash!, request.OldPassword);

            if (!isPwdCorrect)
            {
                return new UpdateUserPasswordCommandResult() { IsSuccess = false, Message = "Old password is incorrect" };
            }

            string newPwdHash = _passHasher.HashPassword(request.NewPassword);
            _user.PasswordHash = newPwdHash;
            await _userRepository.UpdateAsync(_user);

            return new UpdateUserPasswordCommandResult() { IsSuccess = true, Message = "Password updated successfully" };



        }
    }
}
