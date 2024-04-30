using Business.Interface;
using Domain.Core;
using Domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class MQTTUserRepository : BaseRepository<MQTTUser>
    {
        public MQTTUserRepository(IoTHubContext context) : base(context)
        {
        }
    }
}
