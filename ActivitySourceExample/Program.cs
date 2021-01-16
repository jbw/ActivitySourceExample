using System.Diagnostics;
using ActivitySourceExample;
using ActivitySourceExample.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using MassTransit;

Activity.DefaultIdFormat = ActivityIdFormat.W3C;

await Host.CreateDefaultBuilder()
    .ConfigureServices((hostContext, services) =>
    {

        services.AddMassTransit(x =>
        {
            x.AddConsumer<ExampleConsumer>();

            x.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        });

        services.AddMassTransitHostedService();

        services.AddOpenTelemetryTracing((provider, builder) =>
        {
            builder
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("ActivitySourceExample.Console"))
                .AddInstrumentationExample()
                .AddJaegerExporter()
                .AddConsoleExporter();
        });

        services.AddHostedService<ExampleService>();

    })
    .Build()
    .RunAsync();

