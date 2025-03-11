using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Auth0.AspNetCore.Authentication;
using System.Security.Claims;
using NodaTime;
using Microsoft.EntityFrameworkCore;

using Uzerai.Dotnet.Playground.Model.Authentication;
using Uzerai.Dotnet.Playground.DI.Repository;
using Uzerai.Dotnet.Playground.DI.Data.QueryExtensions;
using System.ComponentModel;

namespace Uzerai.Dotnet.Playground.Controllers.Authentication;

[Route("auth")]
public class AuthenticationController : Controller
{
    private readonly UserRepository _userRepository;
    private readonly IClock _clock;
    public AuthenticationController(UserRepository userRepository, IClock clock)
    {
        _userRepository = userRepository;
        _clock = clock;
    }

    /// <summary>
    /// This is the callback endpoint for the Auth0 login.
    /// If a user is authenticated here, it means they have access to the application,
    /// and as such, we can create a user reference in the database to later tie them to 
    /// their related entities.
    /// </summary>
    /// <returns>HTTP 200 OK on success, HTTP 401 Unauthorized on failure.</returns>
    [HttpGet("registration")]
    [Authorize]
    public async Task<IActionResult> Registration()
    {
        var auth0UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var auth0UserEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        var auth0UserUsername = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

        Console.WriteLine($"auth0UserId: {auth0UserId}");
        Console.WriteLine($"auth0UserEmail: {auth0UserEmail}");
        Console.WriteLine($"auth0UserUsername: {auth0UserUsername}");

        if (auth0UserId == null || auth0UserEmail == null)
        {
            return Unauthorized();
        }
        
        var user = await _userRepository.BuildReadonlyQuery()
            .WhereAuth0UserId(auth0UserId)
            .FirstOrDefaultAsync();

        if (user != null)
        {
            user.LastLogin = _clock.GetCurrentInstant();
            user = await _userRepository.UpdateAsync(user);
        }
        else
        {
            user = await _userRepository.CreateAsync(new User()
            {
                Auth0UserId = auth0UserId,
                Email = auth0UserEmail,
                Username = auth0UserUsername ?? auth0UserEmail, // fallback to email as username if none in claims
                LastLogin = _clock.GetCurrentInstant()
            });
        }

        return Json(user);
    }
}