using Microsoft.AspNetCore.Http;
using Dotnet.Playground.Model.Authentication;

namespace Dotnet.Playground.DI.Authorization.UserContext;

public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private const string UserKey = "CurrentUser";

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public User? CurrentUser => _httpContextAccessor.HttpContext?.Items[UserKey] as User;
    public bool IsAuthenticated => CurrentUser != null;
} 