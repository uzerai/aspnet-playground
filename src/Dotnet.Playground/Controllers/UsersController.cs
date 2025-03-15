using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Dotnet.Playground.DI;
using Dotnet.Playground.DI.Repository.Interface;
using Dotnet.Playground.Model.Authentication;

namespace Dotnet.Playground.Controllers;

[Authorize]
[Route("users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IEntityRepository<User> _userRepository;

    public UsersController(IEntityRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet("me")]
    public IActionResult Get()
    {
        return Ok(HttpContext.GetLocalUser());
    }
}