using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Dotnet.Playground.DI;
using Dotnet.Playground.DI.Repository.Interface;
using Dotnet.Playground.Model.Authentication;
using Dotnet.Playground.DI.Authorization.UserContext;

namespace Dotnet.Playground.Controllers;

[Authorize]
[Route("users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IEntityRepository<User> _userRepository;
    private readonly IUserContext _userContext;

    public UsersController(IEntityRepository<User> userRepository, IUserContext userContext)
    {
        _userRepository = userRepository;
        _userContext = userContext;
    }

    [HttpGet("me")]
    public IActionResult Get()
    {
        return Ok(_userContext.CurrentUser);
    }
}