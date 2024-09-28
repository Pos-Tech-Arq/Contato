using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using Prometheus;

namespace Contato.Infra.Configurations;

public static class ConfigureOpenTelemetryExtension
{
    public static void ConfigureOpenTelemetry(this IServiceCollection serviceCollection)
    {
        serviceCollection.UseHttpClientMetrics();
        serviceCollection.AddOpenTelemetry()
            .WithMetrics(metrics =>
            {
                metrics.AddAspNetCoreInstrumentation();
                metrics.AddRuntimeInstrumentation();
                metrics.AddPrometheusExporter();

                metrics.AddMeter("Microsoft.AspNetCore.Hosting", "Microsoft.AspNetCore.Server.Kestrel");
                metrics.AddView("request-duration",
                    new ExplicitBucketHistogramConfiguration
                    {
                        Boundaries = new[]
                        {
                            0, 0.005, 0.01, 0.025, 0.05, 0.075, 0.1, 0.25, 0.5, 0.7, 1, 2.5, 5, 10
                        },
                    });
            });
    }
}