using ActivitySourceExample.Diagnostics;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ActivitySourceExample
{
    public class ExampleService : IHostedService
    {

        public ExampleService()
        {
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {

            var instructmentation = new InstrumentationExmaple();

            var activity = instructmentation.StartActivity();

            instructmentation.StopActivity(activity);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
