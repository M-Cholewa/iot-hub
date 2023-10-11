using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.DeviceConnection
{
    public interface IRpcClient : IDisposable
    {
        public Task<string> CallAsync(string message, CancellationToken cancellationToken = default);

    }
}
