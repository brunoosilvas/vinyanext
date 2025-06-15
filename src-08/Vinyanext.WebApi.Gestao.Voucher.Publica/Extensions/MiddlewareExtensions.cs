using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace Vinyanext.WebApi.Gestao.Voucher.Publico.Extensions;

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
}
