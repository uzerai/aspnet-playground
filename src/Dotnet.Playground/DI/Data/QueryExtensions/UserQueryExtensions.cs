using Dotnet.Playground.Model.Authentication;

namespace Dotnet.Playground.DI.Data.QueryExtensions;

public static class UserQueryExtensions
{
    public static IQueryable<User> WhereAuth0UserId(this IQueryable<User> query, string auth0UserId) => query.Where(x => x.Auth0UserId == auth0UserId);
}
