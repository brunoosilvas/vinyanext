using System.Globalization;
using System.Reflection;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Localization;
using Scalar.AspNetCore;
using Serilog;
using Vinyanext.Application;
using Vinyanext.Infrastructure;
using Vinyanext.WebApi;
using Vinyanext.WebApi.Extensions;
using Hangfire;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services
    .AddApplication()
    .AddPresentation()
    .AddInfrastructure(builder.Configuration, true);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services
    .AddEndpoints(Assembly.GetExecutingAssembly());

WebApplication app = builder.Build();

app.UseRequestLocalization(options =>
{
    const string defaultCulture = "pt-BR";
    CultureInfo[] supportedCultures =
    [
        new CultureInfo(defaultCulture),
        new CultureInfo("en-US")
    ];

    options.DefaultRequestCulture = new RequestCulture(defaultCulture, defaultCulture);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;

    options.RequestCultureProviders =
    [
        new AcceptLanguageHeaderRequestCultureProvider()
    ];

    options.ApplyCurrentCultureToResponseHeaders = true;
});

app.MapEndpoints();

app.MapOpenApi();

app.UseSwaggerUI(options => {
    options.SwaggerEndpoint("/openapi/v1.json", "Vinyanext Gateway v1");
    options.SwaggerEndpoint("/api/sistema/openapi/v1.json", "Vinyanext Sistema v1");
});

app.UseReDoc(options => {
    options.RoutePrefix = "re-doc";
    options.SpecUrl("/openapi/v1.json");
});

app.UseReDoc(options => {
    options.RoutePrefix = "re-doc-sistema";
    options.SpecUrl("/api/sistema/openapi/v1.json");
});

app.MapScalarApiReference(options => {
    options.OpenApiRoutePattern = "/openapi/v1.json";
    options.CdnUrl = "scalar-doc";
}).AllowAnonymous();

app.MapScalarApiReference(options => {
    options.OpenApiRoutePattern = "/api/sistema/openapi/v1.json";
    options.CdnUrl = "scalar-doc-sistema";
}).AllowAnonymous();

app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseRequestContextLogging();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.UseAuthentication();

app.UseAuthorization();

app.MapReverseProxy();

app.UseHangfireDashboard();

await app.RunAsync();

namespace Vinyanext.WebApi
{
    public partial class Program;
}
