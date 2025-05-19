using Hangfire;
using HangfireBasicAuthenticationFilter;
using Vinyanext.WebApi.Middlewares;

namespace Vinyanext.WebApi.Extensions;

public static class MiddlewareExtensions
{
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
            DashboardTitle = "Vinyanext API",            
            Authorization =
            [
                new HangfireCustomBasicAuthenticationFilter
                {
                    User = "admin",
                    Pass = "admin"
                }
            ]
        });
        
        return app;
    }
}
