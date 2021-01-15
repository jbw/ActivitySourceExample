using System;
using System.Diagnostics;
using ActivitySourceExample;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;


Activity.DefaultIdFormat = ActivityIdFormat.W3C;

// Build the trace provider using OpenTelementry.
// Export activity to console and Jaeger.

// See https://www.jaegertracing.io/docs/1.21/getting-started/ and run Jaeger 
// in a Docker comtainer.
using var openTelemetry = Sdk.CreateTracerProviderBuilder()
    .AddSource(ActivityCreator.ActivitySourceName)
    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("ActivitySourceExample.Console"))
    .AddJaegerExporter(options =>
    {
        options.AgentHost = "jaeger";
        options.AgentPort = 6831;
    })
    .AddConsoleExporter()
    .Build();

// Created activity will be exported to console and Jaeger.
Listener.CreateActivityListener();
ActivityCreator.CreateActivity();

Console.ReadLine();
