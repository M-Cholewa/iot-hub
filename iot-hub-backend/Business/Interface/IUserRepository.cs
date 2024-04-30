using Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IUserRepository
    {
        public Task<User?> GetByEmailAsync(string email);
        public Task AddDeviceAsync(User user, Device device);
        public Task GrantRoleAsync(User user, Role role);
        public Task RevokeRoleAsync(User user, Role role);

    }
}
