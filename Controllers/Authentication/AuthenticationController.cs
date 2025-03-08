using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Auth0.AspNetCore.Authentication;
using System.Security.Claims;

namespace Playground.Controllers.Authentication;

[Route("auth")]
public class AuthenticationController : Controller
{
    [HttpGet("callback")]
    public IActionResult Callback()
    {
        return Ok();
    }

    [HttpGet("login")]
    public async Task Login(string returnUrl = "/")
    {
        var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
            .WithScope("openid profile email offline_access")
            // Indicate here where Auth0 should redirect the user after a login.
            // Note that the resulting absolute Uri must be added to the
            // **Allowed Callback URLs** settings for the app.
            .WithRedirectUri(returnUrl)
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