using Business.Interface;
using Domain.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class RoleRepository : BaseRepository<Domain.Core.Role>
    {

        public RoleRepository(IoTHubContext context) : base(context)
        {
        }

        public async Task<Domain.Core.Role?> GetByKeyAsync(string key)
        {
            return await _context.Roles.Where(r => r.Key == key).FirstOrDefaultAsync();
        }


    }
}
