using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Dotnet.Playground.DI.Repository.Interface;
using Dotnet.Playground.Model.Authentication;
using Dotnet.Playground.DI.Repository;

namespace Dotnet.Playground.DI.Authorization.UserContext;

public static class UserContextExtensions
{
    public static IServiceCollection AddUserContext(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IUserContext, UserContext>();
        services.AddScoped<IEntityRepository<User>, UserRepository>();
        services.AddSingleton<IServiceScopeFactory>(sp => sp.GetRequiredService<IServiceScopeFactory>());
        return services;
    }

    public static IApplicationBuilder UseUserContext(this IApplicationBuilder app)
    {
        return app.UseMiddleware<UserContextMiddleware>();
    }
} 