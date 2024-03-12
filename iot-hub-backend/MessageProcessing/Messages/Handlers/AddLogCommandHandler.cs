using Business.InfluxRepository;
using Business.Repository;
using Domain.InfluxDB;
using MediatR;
using MessageProcessing.Messages.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageProcessing.Messages.Handlers
{
    public class AddLogCommandHandler : IRequestHandler<AddLogCommand>
    {
        private readonly DeviceRepository _deviceRepository;
        private readonly LogRepository _logRepository;
        private readonly GeneralLogRepository _generalLogRepository;

        public AddLogCommandHandler(DeviceRepository deviceRepository, LogRepository logRepository, GeneralLogRepository generalLogRepository)
        {
            _deviceRepository = deviceRepository;
            _logRepository = logRepository;
            _generalLogRepository = generalLogRepository;
        }

        public async Task Handle(AddLogCommand request, CancellationToken cancellationToken)
        {
            var device = await _deviceRepository.GetByClientIdAsync(request.ClientId);

            if (device == null)
            {
                return;
            }

            if (request.Telemetry == null)
            {
                return;
            }

            var logs = new List<Log>();

            foreach (var log in request.Telemetry)
            {
                var logData = new Log
                {
                    DeviceId = device.Id,
                    DateUTC = DateTime.UtcNow,
                    Message = log.Message,
                    Severity = log.Severity
                };

                if (string.IsNullOrEmpty(logData.Severity))
                {
                    logData.Severity = "noSeverity";
                }

                if (string.IsNullOrEmpty(logData.Message))
                {
                    logData.Message = "noMessage";
                }

                logs.Add(logData);
            }

            _logRepository.Add(logs);

            var generalLog = new GeneralLog
            {
                DeviceId = device.Id,
                DateUTC = DateTime.UtcNow,
                Message = "Received Log message"
            };

            _generalLogRepository.Add(generalLog);

            Console.WriteLine($"Received Log message.");

        }
    }
}
