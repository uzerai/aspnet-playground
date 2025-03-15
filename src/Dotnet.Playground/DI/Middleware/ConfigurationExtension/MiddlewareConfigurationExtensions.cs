using Dotnet.Playground.API.DI.Middleware;

namespace Dotnet.Playground.DI.Middleware.ConfigurationExtension;

public static class MiddlewareConfigurationExtensions
{
    /// <summary>
    /// An extension method for registering all in-project middleware.
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseProjectMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<LocalUserContextMiddleware>();

        return app;
    }
}
