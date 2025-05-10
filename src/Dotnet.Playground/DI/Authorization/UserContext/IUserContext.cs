using Dotnet.Playground.Model.Authentication;

namespace Dotnet.Playground.DI.Authorization.UserContext;

public interface IUserContext
{
    User? CurrentUser { get; }
    bool IsAuthenticated { get; }
} 