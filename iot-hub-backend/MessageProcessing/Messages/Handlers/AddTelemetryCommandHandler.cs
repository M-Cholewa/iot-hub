using Business.InfluxRepository;
using Business.Repository;
using Domain.InfluxDB;
using InfluxDB.Client.Api.Domain;
using MediatR;

namespace MessageProcessing.Messages.Handlers
{
    public class AddTelemetryCommandHandler : IRequestHandler<Requests.AddTelemetryCommand>
    {
        private readonly DeviceRepository _deviceRepository;
        private readonly TelemetryRepository _telemetryRepository;

        public AddTelemetryCommandHandler(DeviceRepository deviceRepository, TelemetryRepository telemetryRepository)
        {
            _deviceRepository = deviceRepository;
            _telemetryRepository = telemetryRepository;
        }

        public async Task Handle(Requests.AddTelemetryCommand request, CancellationToken cancellationToken)
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

            var telemetryData = new List<Telemetry>();

            foreach (var telemetry in request.Telemetry)
            {
                var telemetryDataItem = new Telemetry
                {
                    DeviceId = device.Id,
                    DateUTC = DateTime.UtcNow,
                    FieldName = telemetry.FieldName,
                    FieldUnit = telemetry.Unit,
                    FieldValue = telemetry.Value
                };
                telemetryData.Add(telemetryDataItem);
            }


            _telemetryRepository.Add(telemetryData);

            Console.WriteLine($"Received Telemetry message.");
        }
    }
}
