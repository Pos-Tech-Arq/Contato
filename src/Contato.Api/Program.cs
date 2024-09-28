using Contato.Api.Endpoints;
using Contato.Api.Requests.Validators;
using Contato.Infra.Configurations;
using Contato.Infra.Contexts;
using Contato.Infra.ExternalServices.BrasilApiService;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.ConfigureRabbit();
builder.Services.ConfigureRepositories();
builder.Services.AddApplicationServices();
builder.Services.AddBrasilApiClientExtensions(builder.Configuration);
builder.Services.AddValidatorsFromAssemblyContaining<CriarContatoRequestValidator>();
builder.AddFluentValidationEndpointFilter();
builder.Services.AddHealthChecks().ForwardToPrometheus();
builder.Services.ConfigureOpenTelemetry();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseHttpMetrics();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health");
app.RegisterContatosEndpoints();
app.MapMetrics();

app.Run();

public partial class Program
{
}