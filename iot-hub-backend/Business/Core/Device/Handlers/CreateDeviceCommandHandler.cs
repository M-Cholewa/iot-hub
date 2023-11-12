using Business.Core.Device.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Core.Device.Handlers
{
    public class CreateDeviceCommandHandler : IRequestHandler<CreateDeviceCommand, CreateDeviceCommandResult>
    {
        public Task<CreateDeviceCommandResult> Handle(CreateDeviceCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
