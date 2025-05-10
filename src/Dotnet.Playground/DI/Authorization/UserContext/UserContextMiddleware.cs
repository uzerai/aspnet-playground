using System.Security.Claims;
using Dotnet.Playground.DI.Authorization.UserContext;
using Dotnet.Playground.DI.Repository.Interface;
using Dotnet.Playground.Model.Authentication;

namespace Dotnet.Playground.DI.Authorization.UserContext;

public class UserContextMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<UserContextMiddleware> _logger;
    private readonly IUserManagementService _userManagementService;

    public UserContextMiddleware(
        RequestDelegate next,
        ILogger<UserContextMiddleware> logger,
        IUserManagementService userManagementService)
    {
        _next = next;
        _logger = logger;
        _userManagementService = userManagementService;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var auth0Id = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!string.IsNullOrEmpty(auth0Id))
            {
                var email = context.User.FindFirst(ClaimTypes.Email)?.Value;
                var username = context.User.FindFirst(ClaimTypes.Name)?.Value;

                if (email != null)
                {
                    var user = await _userManagementService.GetOrCreateUserAsync(auth0Id, email, username);
                    await _userManagementService.UpdateLastLoginAsync(user);
                    context.Items["CurrentUser"] = user;
                }
            }
        }

        await _next(context);
    }
} 