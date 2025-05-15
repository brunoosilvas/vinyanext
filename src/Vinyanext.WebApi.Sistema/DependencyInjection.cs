using System;

namespace Vinyanext.WebApi.Sistema;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {   
        services.AddOpenApi();

        return services;
    }

}
