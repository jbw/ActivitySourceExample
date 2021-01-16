using System;
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


Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {

        services.AddOpenTelemetryTracing((provider, builder) =>
        {
            builder
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("ActivitySourceExample.Console"))
                .AddInstrumentationExample()
                .AddJaegerExporter()
                .AddConsoleExporter();
        });

        services.AddMassTransit(x =>
        {
            x.AddConsumer<ExampleConsumer>();

            x.UsingInMemory((context, cfg) =>
            {        
                cfg.ConfigureEndpoints(context);
            });
        });

        services.AddMassTransitHostedService();

        services.AddHostedService<ExampleService>();

    })
    .Build()
    .Run();
