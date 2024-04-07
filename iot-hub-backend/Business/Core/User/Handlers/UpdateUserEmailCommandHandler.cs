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
    public class UpdateUserEmailCommandHandler : IRequestHandler<UpdateUserEmailCommand, UpdateUserEmailCommandResult>
    {
        private readonly UserRepository _userRepository;

        public UpdateUserEmailCommandHandler(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UpdateUserEmailCommandResult> Handle(UpdateUserEmailCommand request, CancellationToken cancellationToken)
        {
            var _user = await _userRepository.GetByIdAsync(request.UserId);

            if (_user == null)
            {
                return new UpdateUserEmailCommandResult() { IsSuccess = false, Message = "User does not exist" };
            }

            _user.Email = request.Email;

            await _userRepository.UpdateAsync(_user);

            return new UpdateUserEmailCommandResult() { IsSuccess = true, Message = "Email updated successfully" };
        }
    }
}
