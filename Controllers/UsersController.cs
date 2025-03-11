using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Uzerai.Dotnet.Playground.DI;
using Uzerai.Dotnet.Playground.DI.Repository;

namespace Uzerai.Dotnet.Playground.Controllers;

[Authorize]
[Route("users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly UserRepository _userRepository;

    public UsersController(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet("me")]
    public IActionResult Get()
    {
        return Ok(HttpContext.GetLocalUser());
    }
}