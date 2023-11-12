using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Core.Device.Commands
{
    public class RemoveDeviceCommandResult
    {
        public bool IsSuccess { get; set; }
    }

    public class RemoveDeviceCommand : IRequest<RemoveDeviceCommandResult>
    {
        public Guid Id { get; set; }
    }
}
