using Dotnet.Playground.DI.Repository.Interface;
using Dotnet.Playground.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Playground.Controllers;

[ApiController]
[Route("areas")]
public class AreaController : ControllerBase
{
    private readonly IEntityRepository<Area> _areaRepository;

    public AreaController(IEntityRepository<Area> areaRepository)
    {
        _areaRepository = areaRepository;
    }

    [HttpPost]
    public async Task<ActionResult<Area>> Create(Area area)
    {
        var createdArea = await _areaRepository.CreateAsync(area);
        return CreatedAtAction(nameof(GetById), new { id = createdArea.Id }, createdArea);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Area>> GetById(Guid id)
    {
        var area = await _areaRepository.GetByIdAsync(id);
        if (area == null)
            return NotFound();
        return area;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Area>>> GetAll()
    {
        var areas = await _areaRepository.GetAllAsync();
        return Ok(areas);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, Area area)
    {
        if (id != area.Id)
            return BadRequest();

        await _areaRepository.UpdateAsync(area);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var area = await _areaRepository.GetByIdAsync(id);
        if (area == null)
            return NotFound();

        await _areaRepository.DeleteAsync(area);
        return NoContent();
    }
} 