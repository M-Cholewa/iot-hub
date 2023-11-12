using Business.Core.Device.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Core.Device.Handlers
{
    public class RemoveDeviceCommandHandler : IRequestHandler<RemoveDeviceCommand, RemoveDeviceCommandResult>
    {
        public Task<RemoveDeviceCommandResult> Handle(RemoveDeviceCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
