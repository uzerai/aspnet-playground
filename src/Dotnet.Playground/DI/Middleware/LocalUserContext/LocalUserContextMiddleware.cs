using System.Security.Claims;
using System.Text.Json;
using Auth0.AspNetCore.Authentication.BackchannelLogout;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using Uzerai.Dotnet.Playground.DI;
using Uzerai.Dotnet.Playground.DI.Data.QueryExtensions;
using Uzerai.Dotnet.Playground.DI.Repository;
using Uzerai.Dotnet.Playground.DI.Repository.Interface;
using Uzerai.Dotnet.Playground.Model.Authentication;

namespace Uzerai.Dotnet.Playground.API.DI.Middleware;

/// <summary>
/// Middleware for setting the Database retrieved user identity in the HttpContext.
/// 
/// This is the middleware that provides the link from an externally authorized user
/// to the authenticated identity of the user in our system, in addition to not
/// interfering with the identity provided by the login-provider.
/// </summary>
public class LocalUserContextMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LocalUserContextMiddleware> _logger;

    public LocalUserContextMiddleware(
        RequestDelegate next,
        ILogger<LocalUserContextMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(
        HttpContext context, IEntityRepository<User> userRepository, IClock clock)
    {
        // This happens when an anonymous users requests a resource that is not protected.
        // We can safely skip the rest of the middleware, as we shouldn't need to do anything regarding _current_ user.
        // If the endpoint is protected, the request should be rejected earlier in the chain than here.
        if (context.User.Identity == null || !context.User.Identity.IsAuthenticated)
        {
            await _next(context);
            return;
        }

        var cancellationToken = context.RequestAborted;
        // If the JWT token is authorized for access, we need to check for a local user.
        var userAuth0NameIdentifier = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        // and if there's no identifier, something is wrong with the token (even beyond all format validation)
        if (string.IsNullOrEmpty(userAuth0NameIdentifier))
        {
            _logger.LogError("HttpContext.User.Identity contains no NameIdentifier claim. This should never happen. Skipping local user context middleware.");
            return;
        }

        // If there is a user identifier, we can check for a local user.
        var localIdentity = await userRepository.BuildReadonlyQuery()
            .WhereAuth0UserId(userAuth0NameIdentifier)
            .Include(u => u.OrganizationUsers)
            .ThenInclude(ou => ou.Permissions)
            .FirstOrDefaultAsync(cancellationToken);

        // If there is no local user, we need to create one, since the user is authorized for 
        // access and therefore should be allowed to use the application.
        if (localIdentity == null)
        {
            var auth0UserEmail = context.User.FindFirst(ClaimTypes.Email)!.Value;
            var auth0UserUsername = context.User.FindFirst(ClaimTypes.Name)?.Value;

            _logger.LogInformation("First time login for user {UserAuth0NameIdentifier}. Creating local user.", userAuth0NameIdentifier);
            localIdentity = await userRepository.CreateAsync(new User()
            {
                Auth0UserId = userAuth0NameIdentifier,
                Email = auth0UserEmail,
                Username = auth0UserUsername ?? auth0UserEmail, // fallback to email as username if none in claims
                LastLogin = clock.GetCurrentInstant(),
            });
            
        }
        else
        {
            localIdentity.LastLogin = clock.GetCurrentInstant();
            localIdentity = await userRepository.UpdateAsync(localIdentity);
        }

        // Set the local user identity in the context.
        // We can guarantee its non-nullability.
        context.SetLocalUser(localIdentity!);

        await _next(context);
    }
}