using Scalar.AspNetCore;
using Vinyanext.Application;
using Vinyanext.Infrastructure;

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
    options.SwaggerEndpoint("/api-sistema/openapi/v1.json", "Vinyanext Sistema v1"));

app.UseReDoc(options => {
    options.RoutePrefix = "re-doc";
    options.SpecUrl("/openapi/v1.json");
});

app.MapScalarApiReference("scalar-doc");

app.UseCors();

await app.RunAsync();

namespace Vinyanext.WebApi.Gestao.Voucher.Publico
{
    public partial class Program;
}
