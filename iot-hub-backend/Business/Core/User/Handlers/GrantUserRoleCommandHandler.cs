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
    public class GrantUserRoleCommandHandler : IRequestHandler<GrantUserRoleCommand, GrantUserRoleCommandResult>
    {
        private readonly UserRepository _userRepository;
        private readonly RoleRepository _roleRepository;

        public GrantUserRoleCommandHandler(UserRepository userRepository, RoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<GrantUserRoleCommandResult> Handle(GrantUserRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user == null)
            {
                return new GrantUserRoleCommandResult { IsSuccess = false, Message = "User not found" };
            }

            var role = await _roleRepository.GetByIdAsync(request.RoleId);

            if (role == null)
            {
                return new GrantUserRoleCommandResult { IsSuccess = false, Message = "Role not found" };
            }

            await _userRepository.GrantRoleAsync(user, role);

            return new GrantUserRoleCommandResult { IsSuccess = true };
        }
    }
}
