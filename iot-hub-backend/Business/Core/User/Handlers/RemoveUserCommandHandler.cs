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
    public class RemoveUserCommandHandler : IRequestHandler<RemoveUserCommand, RemoveUserCommandResult>
    {
        private readonly UserRepository _userRepository;

        public RemoveUserCommandHandler(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<RemoveUserCommandResult> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            var usr = await _userRepository.GetByIdAsync(request.UserId);

            if (usr == null)
            {
                return new RemoveUserCommandResult() { IsSuccess = false, Message = "User does not exist" };
            }

            await _userRepository.DeleteAsync(usr);

            return new RemoveUserCommandResult() { IsSuccess = true };
        }
    }
}
