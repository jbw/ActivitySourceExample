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

        public ExampleService(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _publishEndpoint.Publish<ExampleMessage>(new { Id = Guid.NewGuid().ToString() }, cancellationToken);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }
    }
}
