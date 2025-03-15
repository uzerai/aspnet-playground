using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Playground.Controllers;

[Authorize]
[Route("/")]
[ApiController]
public class HomeController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok();
    }
}