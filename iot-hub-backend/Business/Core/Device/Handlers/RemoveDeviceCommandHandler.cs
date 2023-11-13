using Business.Core.Device.Commands;
using Business.Repository;
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
        private readonly DeviceRepository _deviceRepository;


        public RemoveDeviceCommandHandler(DeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public async Task<RemoveDeviceCommandResult> Handle(RemoveDeviceCommand request, CancellationToken cancellationToken)
        {
            var device = await _deviceRepository.GetByIdAsync(request.Id);

            if (device == null)
            {
                return new RemoveDeviceCommandResult() { IsSuccess = false, Message = "Device does not exist" };
            }

             await _deviceRepository.DeleteAsync(device);

            return new RemoveDeviceCommandResult() { IsSuccess = true };
        }
    }
}
