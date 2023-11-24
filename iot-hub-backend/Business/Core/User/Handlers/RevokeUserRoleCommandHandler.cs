using Business.Core.User.Commands;
using Business.Repository;
using MediatR;

namespace Business.Core.User.Handlers
{
    public class RevokeUserRoleCommandHandler : IRequestHandler<RevokeUserRoleCommand, RevokeUserRoleCommandResult>
    {
        private readonly UserRepository _userRepository;
        private readonly RoleRepository _roleRepository;

        public RevokeUserRoleCommandHandler(UserRepository userRepository, RoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<RevokeUserRoleCommandResult> Handle(RevokeUserRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user == null)
            {
                return new RevokeUserRoleCommandResult { IsSuccess = false, Message = "User not found" };
            }

            var role = await _roleRepository.GetByIdAsync(request.RoleId);

            if (role == null)
            {
                return new RevokeUserRoleCommandResult { IsSuccess = false, Message = "Role not found" };
            }

            await _userRepository.RevokeRoleAsync(user, role);

            return new RevokeUserRoleCommandResult { IsSuccess = true };
        }
    }
}
