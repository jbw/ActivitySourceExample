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
        private Instrumentation _instructmentation;

        public ExampleService(Instrumentation instrumentation)
        {
            _instructmentation = instrumentation;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var activity = _instructmentation.StartActivity();
            _instructmentation.StopActivity(activity);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
