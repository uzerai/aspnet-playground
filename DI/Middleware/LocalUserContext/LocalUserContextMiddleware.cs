using System.Security.Claims;
using System.Text.Json;
using Auth0.AspNetCore.Authentication.BackchannelLogout;
using NodaTime;
using Uzerai.Dotnet.Playground.DI;
using Uzerai.Dotnet.Playground.DI.Data.QueryExtensions;
using Uzerai.Dotnet.Playground.DI.Repository;
using Uzerai.Dotnet.Playground.Model.Authentication;

namespace Uzerai.Dotnet.Playground.API.DI.Middleware;

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
      HttpContext context, UserRepository userRepository, IClock clock)
  {
    // If the JWT token is not authorized for access, we never authenticate the user in the context,
    // and as such there's no need to check for a local user.
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
        await context.WriteErrorAsync(403, "User 'sub' claim is required", "Could not check for user locally");
        return;
    }

    // If there is a user identifier, we can check for a local user.
    var localIdentity = userRepository.BuildReadonlyQuery()
        .WhereAuth0UserId(userAuth0NameIdentifier)
        .FirstOrDefault();

    // If there is no local user, we need to create one, since the user is authorized for 
    // access and therefore should be allowed to use the application.
    if (localIdentity == null)
    {
        var auth0UserEmail = context.User.FindFirst(ClaimTypes.Email)!.Value;
        var auth0UserUsername = context.User.FindFirst(ClaimTypes.Name)?.Value;
        
        localIdentity = await userRepository.CreateAsync(new User()
            {
                Auth0UserId = userAuth0NameIdentifier,
                Email = auth0UserEmail,
                Username = auth0UserUsername ?? auth0UserEmail, // fallback to email as username if none in claims
                LastLogin = clock.GetCurrentInstant()
            });
    } else {
      localIdentity.LastLogin = clock.GetCurrentInstant();
      localIdentity = await userRepository.UpdateAsync(localIdentity);
    }

    // Set the local user identity in the context.
    // We can guarantee its non-nullability.
    context.SetLocalUser(localIdentity!);

    await _next(context);
  }
}