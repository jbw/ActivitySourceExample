using OpenTelemetry.Trace;


namespace ActivitySourceExample.Diagnostics
{
    public static class TraceProviderBuilderExtension
    {
        public static TracerProviderBuilder AddInstrumentationExample(this TracerProviderBuilder builder) => 
            builder.AddSource(InstrumentationExmaple.ActivitySourceName).AddInstrumentation(() => new InstrumentationExmaple());

    }
}
