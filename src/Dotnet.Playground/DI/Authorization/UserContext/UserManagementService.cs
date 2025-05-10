using Dotnet.Playground.DI.Data.QueryExtensions;
using Dotnet.Playground.DI.Repository.Interface;
using Dotnet.Playground.Model.Authentication;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace Dotnet.Playground.DI.Authorization.UserContext;

public interface IUserManagementService
{
    Task<User> GetOrCreateUserAsync(string auth0Id, string email, string? username);
    Task UpdateLastLoginAsync(User user);
}

public class UserManagementService : IUserManagementService
{
    private readonly IUserRepository _userRepository;
    private readonly IClock _clock;
    private readonly ILogger<UserManagementService> _logger;

    public UserManagementService(
        IUserRepository userRepository,
        IClock clock,
        ILogger<UserManagementService> logger)
    {
        _userRepository = userRepository;
        _clock = clock;
        _logger = logger;
    }

    public async Task<User> GetOrCreateUserAsync(string auth0Id, string email, string? username)
    {
        var user = await _userRepository.BuildReadonlyQuery()
            .WhereAuth0UserId(auth0Id)
            .Include(u => u.OrganizationUsers)
            .ThenInclude(ou => ou.Permissions)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            _logger.LogInformation("First time login for user {UserAuth0NameIdentifier}. Creating local user.", auth0Id);
            user = await _userRepository.CreateAsync(new User()
            {
                Auth0UserId = auth0Id,
                Email = email,
                Username = username ?? email,
                LastLogin = _clock.GetCurrentInstant(),
            });
        }

        return user;
    }

    public async Task UpdateLastLoginAsync(User user)
    {
        user.LastLogin = _clock.GetCurrentInstant();
        await _userRepository.UpdateAsync(user);
    }
} 