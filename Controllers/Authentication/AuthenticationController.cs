using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Auth0.AspNetCore.Authentication;
using System.Security.Claims;
using Playground.DI.Repository;
using Playground.Model.Authentication;
using NodaTime;

namespace Playground.Controllers.Authentication;

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
    [HttpGet("callback")]
    [Authorize]
    public async Task<IActionResult> Callback()
    {
        var auth0UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var auth0UserEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        var auth0UserUsername = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        
        Console.WriteLine($"Auth0 User ID: {auth0UserId}");
        Console.WriteLine($"Auth0 User Email: {auth0UserEmail}");
        Console.WriteLine($"Auth0 User Username: {auth0UserUsername}");

        if (auth0UserId == null || auth0UserEmail == null)
        {
            return Unauthorized();
        }

        var user = await _userRepository.CreateAsync(new User()
        {
            Auth0UserId = auth0UserId,
            Email = auth0UserEmail,
            Username = auth0UserEmail,
            LastLogin = _clock.GetCurrentInstant()
        });

        return Ok();
    }

    [HttpGet("login")]
    public async Task Login()
    {
        var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
            .WithScope("openid profile email offline_access")
            // Indicate here where Auth0 should redirect the user after a login.
            // Note that the resulting absolute Uri must be added to the
            // **Allowed Callback URLs** settings for the app.
            .WithRedirectUri("https://localhost:5016/auth/callback")
            .Build();

        await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);


    }

    [HttpGet("logout")]
    [Authorize]
    public IActionResult Logout()
    {
        return Ok();
    }

    [HttpGet("profile")]
    [Authorize]
    public IActionResult Profile()
    {
        return Json(new
        {
            User.Identity,
            IsMaintainer = User.IsInRole("Maintainer"),
            Email = User.FindFirst(ClaimTypes.Email)?.Value,
            Claims = User.Claims.Select(c => new { c.Type, c.Value, c.ValueType, c.Properties }),
            Permissions = User.FindFirst("permissions")?.Value
        });
    }
}