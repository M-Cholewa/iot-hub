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
            return await _context.Users.Include(u => u.Roles).Where(u => u.Email == email).FirstAsync();
        }
    }
}
