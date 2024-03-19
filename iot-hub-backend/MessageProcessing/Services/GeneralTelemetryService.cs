using Domain.InfluxDB;
using MediatR;
using MessageProcessing.Messages.Commands;
using Microsoft.Extensions.Hosting;

namespace MessageProcessing.Services
{
    public class GeneralTelemetryService : IHostedService
    {
        private readonly TimeSpan TIMER_PERIOD = TimeSpan.FromHours(1);
        private DateTime _lastTimerRun;
        private Timer? _timer;
        private readonly IMediator _mediator;

        public GeneralTelemetryService(IMediator mediator)
        {
            _mediator = mediator;
            _lastTimerRun = DateTime.UtcNow - TIMER_PERIOD;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new(TimedService, null, TimeSpan.Zero, TIMER_PERIOD); // every <_period> milliseconds

            return Task.CompletedTask;

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            _timer?.Dispose();
            return Task.CompletedTask;
        }

        private async void TimedService(object? state)
        {
            var sinceUTC = _lastTimerRun;
            var toUTC = DateTime.UtcNow;
            _lastTimerRun = toUTC; // update last run time

            var _cmd = new CreateGeneralTelemetriesCommand { SinceUTC = sinceUTC, ToUTC = toUTC };
            await _mediator.Send(_cmd);
        }

    }
}
