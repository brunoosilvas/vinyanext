using Vinyanext.WebApi.Handlers;

namespace Vinyanext.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddOpenApi();

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.WithOrigins("*")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        services.AddExceptionHandler<GlobalExceptionHandler>();

        services.AddProblemDetails();

        return services;
    }

}
