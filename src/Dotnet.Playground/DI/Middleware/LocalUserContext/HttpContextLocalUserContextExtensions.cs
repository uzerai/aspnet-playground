using Dotnet.Playground.Model.Authentication;

namespace Dotnet.Playground.DI;

/// <summary>
/// This class contains extension methods for the HttpContext class, specifically related
/// to the `LocalUserContext` middleware.
/// 
/// We wished to keep the auth0 HttpContext.User.Identity separate from the local user context; so that
/// the auth0 identity is not polluted with local user data (and vice versa)./
/// 
/// These extension methods are used to get and set the local user (as User class instance) in the http context.
/// </summary>
public static class HttpContextLocalUserContextExtensions
{
    /// <summary>
    /// Get the local user from the HttpContext.
    /// 
    /// This 
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static User GetLocalUser(this HttpContext context)
    {
        return (User)context.Items["LocalUserIdentity"]!;
    }

    /// <summary>
    /// Setter for local user in the HttpContext.
    /// 
    /// It is strongly recommended that this not be used anywhere other than the LocalUserContextMiddleware.
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="user"></param>
    public static void SetLocalUser(this HttpContext context, User user)
    {
        context.Items["LocalUserIdentity"] = user;
    }
}