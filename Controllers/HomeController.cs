using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Playground.Controllers;

[Route("/")]
[Authorize]
public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok();
    }
}