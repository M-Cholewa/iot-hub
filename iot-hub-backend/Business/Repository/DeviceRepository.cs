using Business.Interface;
using Domain.Core;
using Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace Business.Repository
{
    public class DeviceRepository : BaseRepository<Device>, IRepository
    {
        public DeviceRepository(IoTHubContext context) : base(context)
        {

        }

        public async Task<Device?> GetByClientIdAsync(Guid Clientid)
        {
            return await _context.Set<Device>().Where(x => x.MQTTUser != null && x.MQTTUser.ClientID == Clientid).FirstOrDefaultAsync();
        }
    }
}
