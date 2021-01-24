using OpenTelemetry.Trace;

namespace ActivitySourceExample.Diagnostics
{
    public static class TraceProviderBuilderExtension
    {
        public static TracerProviderBuilder AddInstrumentationExample(this TracerProviderBuilder builder)
        {
            return builder
                .AddSource(MassTransitActivity.ActivitySourceName)
                .AddInstrumentation(() => new InstrumentationExample());
        }

    }
}
