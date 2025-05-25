using System.Reflection;
using Scalar.AspNetCore;
using Vinyanext.Application;
using Vinyanext.Infrastructure;
using Vinyanext.WebApi.Almoxarifado;
using Vinyanext.WebApi.Almoxarifado.Endpoints;
using Vinyanext.WebApi.Almoxarifado.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddPresentation()
    .AddInfrastructure(builder.Configuration);

builder.Services
    .AddEndpoints(Assembly.GetExecutingAssembly());

WebApplication app = builder.Build();

app.UsePathBase("/api-almoxarifado");

app.MapEndpoints();

app.MapOpenApi();

app.UseSwaggerUI(options =>
    options.SwaggerEndpoint("/api-almoxarifado/openapi/v1.json", "Vinyanext Almoxarifado v1"));

app.UseReDoc(options => {
    options.RoutePrefix = "re-doc";
    options.SpecUrl("/openapi/v1.json");
});

app.MapScalarApiReference("scalar-doc")
    .AllowAnonymous();

await app.RunAsync();

namespace Vinyanext.WebApi.Almoxarifado
{
    public partial class Program;
}
