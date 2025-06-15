using System.Globalization;
using Hangfire;
using HangfireBasicAuthenticationFilter;
using Microsoft.AspNetCore.Localization;
using Vinyanext.WebApi.Middlewares;

namespace Vinyanext.WebApi.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseIntercionalization(this IApplicationBuilder app)
    {
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

        return app;
    }

    public static IApplicationBuilder UseRequestContextLogging(this IApplicationBuilder app)
    {
        app.UseMiddleware<RequestContextLoggingMiddleware>();

        return app;
    }

    public static IApplicationBuilder UseHangfireDashboard(this IApplicationBuilder app, IConfiguration configuration)
    {
        app.UseHangfireDashboard("/hangfire", new DashboardOptions()
        {
            DisplayStorageConnectionString = false,
            DashboardTitle = configuration.GetSection("Hangfire:Dashboard:Title").Get<string>()!,
            Authorization =
            [
                new HangfireCustomBasicAuthenticationFilter
                {
                    User = configuration.GetSection("Hangfire:Dashboard:User").Get<string>()!,
                    Pass = configuration.GetSection("Hangfire:Dashboard:Password").Get<string>()!
                }
            ]
        });

        return app;
    }
}
