using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core
{
    public class Device
    {
        public Guid Id { get; set; }
        public string? ApiKey { get; set; }
        public User? Owner { get; set; }
        public string? DeviceTwin { get; set; }
    }
}
