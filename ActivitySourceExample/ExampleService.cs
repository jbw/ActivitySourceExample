using ActivitySourceExample.Diagnostics;
using MassTransit;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ActivitySourceExample
{

    public class ExampleService : IHostedService
    {
        private IPublishEndpoint _publishEndpoint;

        public ExampleService(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;

        }


        public async Task StartAsync(CancellationToken cancellationToken)
        {

            await _publishEndpoint.Publish<ExampleMessage>(new { Id = Guid.NewGuid().ToString() }, cancellationToken);

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
