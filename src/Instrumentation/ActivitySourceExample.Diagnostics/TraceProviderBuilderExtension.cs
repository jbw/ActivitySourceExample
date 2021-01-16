using OpenTelemetry.Trace;

namespace ActivitySourceExample.Diagnostics
{
    public static class TraceProviderBuilderExtension
    {
        public static TracerProviderBuilder AddInstrumentationExample(this TracerProviderBuilder builder)
        {
            var sourceName = InstrumentationExample.ActivitySourceName;

            return builder
                .AddSource(sourceName)
                .AddInstrumentation(() => new InstrumentationExample());
        }

    }
}
