using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace Vinyanext.WebApi.Sistema.Extensions;

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
        //app.UseMiddleware<RequestContextLoggingMiddleware>();

        return app;
    }

}
