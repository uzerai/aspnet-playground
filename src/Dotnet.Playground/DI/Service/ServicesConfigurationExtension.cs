namespace Dotnet.Playground.DI.Service;

public static class ServicesConfigurationExtension
{
    public static IServiceCollection AddImageStorageService(this IServiceCollection services)
    {
        services.AddScoped<IImageStorageService, ImageStorageService>();

        return services;
    }
}
