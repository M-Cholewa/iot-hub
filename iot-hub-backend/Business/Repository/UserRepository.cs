using Business.Interface;
using Domain.Core;
using Domain.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class UserRepository : BaseRepository<User>, IRepository
    {
        public UserRepository(IoTHubContext context) : base(context)
        {
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task AddDevice(User user, Device device)
        {
            user.Devices ??= new List<Device>();
            user.Devices.Add(device);
            await _context.SaveChangesAsync();
        }

        public async Task AddRole(User user, Role role)
        {
            user.Roles ??= new List<Role>();
            user.Roles.Add(role);
            await _context.SaveChangesAsync();
        }


    }


}
