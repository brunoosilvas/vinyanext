using System;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Vinyanext.Application.Abstractions.Behaviros;
using Vinyanext.Application.Abstractions.Services.Sistema;
using Vinyanext.Application.Services.Sistema;

namespace Vinyanext.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

            config.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
            config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
        });

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);

        services.AddTransient<ILoginService, LoginService>();

        return services;
    }
}
