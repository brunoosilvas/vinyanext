using System.Reflection;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
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

app.UseIntercionalization();

app.MapEndpoints();

app.MapOpenApi();

app.UseSwaggerUI(options => {

    options.SwaggerEndpoint("/openapi/v1.json", "Vinyanext Gateway v1");
    options.SwaggerEndpoint("/api-almoxarifado/openapi/v1.json", "Vinyanext Almoxarifado v1");
    options.SwaggerEndpoint("/api-sistema/openapi/v1.json", "Vinyanext Sistema v1");
});

app.UseReDoc(options =>
{
    options.RoutePrefix = "redoc";
    options.SpecUrl("/openapi/v1.json");
});

app.UseReDoc(options => {
    options.RoutePrefix = "redoc-almoxarifado";
    options.SpecUrl("/api-almoxarifado/openapi/v1.json");
});

app.UseReDoc(options => {
    options.RoutePrefix = "redoc-sistema";
    options.SpecUrl("/api-sistema/openapi/v1.json");
});

app.MapScalarApiReference("/scalar", options =>
{
    options.OpenApiRoutePattern = "/openapi/v1.json";
});

app.MapScalarApiReference("/scalar-almoxarifado", options =>
{
    options.OpenApiRoutePattern = "/api-almoxarifado/openapi/v1.json";
});

app.MapScalarApiReference("/scalar-sistema", options =>
{
    options.OpenApiRoutePattern = "/api-sistema/openapi/v1.json";
});

app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseCors();

app.UseRequestContextLogging();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.UseAuthentication();

app.UseAuthorization();

app.MapReverseProxy();

app.UseHangfireDashboard(app.Configuration);

await app.RunAsync();

namespace Vinyanext.WebApi
{
    public partial class Program;
}
