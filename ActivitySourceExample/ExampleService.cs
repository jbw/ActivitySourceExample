using MassTransit;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ActivitySourceExample
{

    public class ExampleService : IHostedService, IDisposable
    {
        private IPublishEndpoint _publishEndpoint;
        private Timer _timer;

        public ExampleService(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {

            // Publish the message every 30 seconds.
            _timer = new Timer(
                (e) => _publishEndpoint.Publish<ExampleMessage>(new { Id = Guid.NewGuid().ToString() }, cancellationToken),
                null,
                TimeSpan.Zero,
                TimeSpan.FromSeconds(30));

            return Task.CompletedTask;

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
