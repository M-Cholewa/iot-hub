using Business.Core.Device.Commands;
using Business.InfluxRepository;
using Domain.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Core.Device.Handlers
{
    public class GetDeviceTelemetryCommandHandler : IRequestHandler<GetDeviceTelemetryCommand, GetDeviceTelemetryCommandResult>
    {

        private TelemetryRepository _telemetryRepository;
        private GeneralLogRepository _generalLogRepository;

        public GetDeviceTelemetryCommandHandler(TelemetryRepository telemetryRepository, GeneralLogRepository generalLogRepository)
        {
            _telemetryRepository = telemetryRepository;
            _generalLogRepository = generalLogRepository;
        }

        public async Task<GetDeviceTelemetryCommandResult> Handle(GetDeviceTelemetryCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask; // bless async

            var fwVersionRec = _telemetryRepository.GetLatest(request.DeviceId, Domain.InfluxDB.Telemetries.FW_VERSION);
            var uptimeRec = _telemetryRepository.GetLatest(request.DeviceId, Domain.InfluxDB.Telemetries.UPTIME_S);
            var statusRec = _telemetryRepository.GetLatest(request.DeviceId, Domain.InfluxDB.Telemetries.STATUS);
            var lastActivityRec = _generalLogRepository.GetLastN(request.DeviceId, 1);

            string fwVersion = "?";
            uint uptime = 0;
            string status = "?";
            DateTime lastActivity = DateTime.MinValue;


            try
            {
                uptime = Convert.ToUInt32(uptimeRec?.FieldValue ?? "0");
                fwVersion = fwVersionRec?.FieldValue?.ToString() ?? "?";
                status = statusRec?.FieldValue?.ToString() ?? "?";
                lastActivity = lastActivityRec?.DateUTC ?? DateTime.MinValue;
            }
            catch { }

            return new GetDeviceTelemetryCommandResult()
            {
                DeviceTelemetry = new DeviceTelemetry()
                {
                    FirmwareVersion = fwVersion,
                    UptimeS = uptime,
                    Status = status,
                    LastActivityUTC = lastActivity
                }
            };

        }
    }
}
