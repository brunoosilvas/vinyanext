using System.Reflection;
using Scalar.AspNetCore;
using Vinyanext.Application;
using Vinyanext.Infrastructure;
using Vinyanext.WebApi.Sistema;
using Vinyanext.WebApi.Sistema.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddPresentation()
    .AddInfrastructure(builder.Configuration);

builder.Services
    .AddEndpoints(Assembly.GetExecutingAssembly());

WebApplication app = builder.Build();

app.UseIntercionalization();

app.UsePathBase("/api-sistema");

app.MapEndpoints();

app.MapOpenApi();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("openapi/v1.json", "Vinyanext Sistema v1");
    options.SupportedSubmitMethods([]);
});

app.UseReDoc(options => {
    options.RoutePrefix = "redoc";
    options.SpecUrl("/openapi/v1.json");
});

app.MapScalarApiReference("scalar");

app.UseCors();

await app.RunAsync();

namespace Vinyanext.WebApi.Sistema
{
    public partial class Program;
}
