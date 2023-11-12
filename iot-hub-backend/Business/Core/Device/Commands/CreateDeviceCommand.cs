using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Core.Device.Commands
{
    public class CreateDeviceCommandResult
    {
        bool IsSuccess { get; set; }
        Domain.Core.Device? ResultDevice { get; set; }
    }

    public class CreateDeviceCommand : IRequest<CreateDeviceCommandResult>
    {
        public Domain.Core.Device? Device { get; set; }
    }
}
