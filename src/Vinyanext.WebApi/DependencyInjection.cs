using Vinyanext.WebApi.Infrastructure;

namespace Vinyanext.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {           
        services.AddOpenApi();

        services.AddExceptionHandler<GlobalExceptionHandler>();
        
        services.AddProblemDetails();

        return services;
    }

}
