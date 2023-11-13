using Business.Interface;
using Domain.Core;
using Domain.Data;

namespace Business.Repository
{
    public class DeviceRepository : BaseRepository<Device>, IRepository
    {
        public DeviceRepository(IoTHubContext context) : base(context)
        {
        }
    }
}
