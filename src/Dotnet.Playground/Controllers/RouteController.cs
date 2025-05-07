using Dotnet.Playground.DI.Repository.Interface;
using Dotnet.Playground.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Route = Dotnet.Playground.Model.Route;

namespace Dotnet.Playground.Controllers;

[ApiController]
[Route("routes")]
public class RouteController : ControllerBase
{
    private readonly IEntityRepository<Route> _routeRepository;

    public RouteController(IEntityRepository<Route> routeRepository)
    {
        _routeRepository = routeRepository;
    }

    [HttpPost]
    public async Task<ActionResult<Route>> Create(Route route)
    {
        var createdRoute = await _routeRepository.CreateAsync(route);
        return CreatedAtAction(nameof(GetById), new { id = createdRoute.Id }, createdRoute);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Route>> GetById(Guid id)
    {
        var route = await _routeRepository.GetByIdAsync(id);
        if (route == null)
            return NotFound();
        return route;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Route>>> GetAll()
    {
        var routes = await _routeRepository.GetAllAsync();
        return Ok(routes);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, Route route)
    {
        if (id != route.Id)
            return BadRequest();

        await _routeRepository.UpdateAsync(route);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var route = await _routeRepository.GetByIdAsync(id);
        if (route == null)
            return NotFound();

        await _routeRepository.DeleteAsync(route);
        return NoContent();
    }
} 