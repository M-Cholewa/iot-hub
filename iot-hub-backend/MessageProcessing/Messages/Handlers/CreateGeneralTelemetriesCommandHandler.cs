using Business.InfluxRepository;
using Business.Repository;
using Domain.InfluxDB;
using MediatR;
using MessageProcessing.Messages.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageProcessing.Messages.Handlers
{
    public class CreateGeneralTelemetriesCommandHandler : IRequestHandler<CreateGeneralTelemetriesCommand>
    {
        private readonly UserRepository _userRepository;
        private readonly LogRepository _logRepository;
        private readonly GeneralTelemetryRepository _generalTelemetryRepository;
        private readonly GeneralLogRepository _generalLogRepository;
        private readonly TelemetryRepository _telemetryRepository;

        public CreateGeneralTelemetriesCommandHandler(UserRepository userRepository, LogRepository logRepository, GeneralTelemetryRepository generalTelemetryRepository, GeneralLogRepository generalLogRepository, TelemetryRepository telemetryRepository)
        {
            _userRepository = userRepository;
            _logRepository = logRepository;
            _generalTelemetryRepository = generalTelemetryRepository;
            _generalLogRepository = generalLogRepository;
            _telemetryRepository = telemetryRepository;
        }

        public async Task Handle(CreateGeneralTelemetriesCommand request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();

            if (users == null)
            {
                return;
            }



            foreach (var user in users)
            {
                var userDevices = user.Devices;
                int warnings = 0;
                int errors = 0;
                int infos = 0;
                int devicesOnline = 0;
                int thisHourLoggedMessages = 0;

                if (userDevices == null || userDevices.Count == 0)
                {
                    continue;
                }

                foreach (var device in userDevices)
                {
                    var logs = _logRepository.GetAll(device.Id, request.SinceUTC, request.ToUTC);

                    warnings += logs.Where(l => l.Severity == LogSeverity.WARNING).Count();
                    errors += logs.Where(l => l.Severity == LogSeverity.ERROR).Count();
                    infos += logs.Where(l => l.Severity == LogSeverity.INFO).Count();

                    var statusLogs = _telemetryRepository.Get(device.Id, Telemetries.STATUS, request.SinceUTC, request.ToUTC);
                    var wasOnline = statusLogs.Any(l => l.FieldValue.ToString() == Telemetries.STATUS_ONLINE);

                    if (wasOnline)
                    {
                        devicesOnline++;
                    }

                    var loggedMessages = _generalLogRepository.GetAll(device.Id, request.SinceUTC, request.ToUTC);
                    thisHourLoggedMessages += loggedMessages.Count;
                }

                var olmGenTelem = _generalTelemetryRepository.GetLatest(user.Id, GeneralTelemetries.SUMMED_MESSAGE_COUNT);
                var olmObj = olmGenTelem?.FieldValue;
                int overallLoggedMessages = 0;
                try
                {
                    if (olmObj != null)
                    {
                        overallLoggedMessages = Convert.ToInt32(olmObj);
                    }
                }
                catch { }

                _generalTelemetryRepository.Add(new GeneralTelemetry
                {
                    UserId = user.Id,
                    FieldName = GeneralTelemetries.HOUR_INFOS,
                    FieldValue = infos,
                    DateUTC = DateTime.UtcNow
                });

                _generalTelemetryRepository.Add(new GeneralTelemetry
                {
                    UserId = user.Id,
                    FieldName = GeneralTelemetries.HOUR_WARNINGS,
                    FieldValue = warnings,
                    DateUTC = DateTime.UtcNow
                });

                _generalTelemetryRepository.Add(new GeneralTelemetry
                {
                    UserId = user.Id,
                    FieldName = GeneralTelemetries.HOUR_ERRORS,
                    FieldValue = errors,
                    DateUTC = DateTime.UtcNow
                });

                _generalTelemetryRepository.Add(new GeneralTelemetry
                {
                    UserId = user.Id,
                    FieldName = GeneralTelemetries.HOUR_DEVICES_ONLINE,
                    FieldValue = devicesOnline,
                    DateUTC = DateTime.UtcNow
                });

                _generalTelemetryRepository.Add(new GeneralTelemetry
                {
                    UserId = user.Id,
                    FieldName = GeneralTelemetries.HOUR_LOGGED_MESSAGES,
                    FieldValue = thisHourLoggedMessages,
                    DateUTC = DateTime.UtcNow
                });

                _generalTelemetryRepository.Add(new GeneralTelemetry
                {
                    UserId = user.Id,
                    FieldName = GeneralTelemetries.SUMMED_MESSAGE_COUNT,
                    FieldValue = overallLoggedMessages + thisHourLoggedMessages,
                    DateUTC = DateTime.UtcNow
                });


            }
        }
    }
}
