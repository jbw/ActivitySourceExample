using OpenTelemetry.Trace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivitySourceExample.Diagnostics
{
    public static class TraceProviderBuilderExtension
    {
        public static TracerProviderBuilder AddInstrumentationExample(this TracerProviderBuilder builder)
        {
            builder
                .AddSource(Instrumentation.ActivitySourceName)
                .AddInstrumentation(() => new Instrumentation());

            return builder;

        }
    }
}
