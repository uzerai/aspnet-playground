using Dotnet.Playground.DI.Repository.Interface;
using Dotnet.Playground.Model;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Playground.Controllers;

[ApiController]
[Route("sectors")]
public class SectorController : ControllerBase
{
    private readonly IEntityRepository<Sector> _sectorRepository;

    public SectorController(IEntityRepository<Sector> sectorRepository)
    {
        _sectorRepository = sectorRepository;
    }

    [HttpPost]
    public async Task<ActionResult<Sector>> Create(Sector sector)
    {
        var createdSector = await _sectorRepository.CreateAsync(sector);
        return CreatedAtAction(nameof(GetById), new { id = createdSector.Id }, createdSector);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Sector>> GetById(Guid id)
    {
        var sector = await _sectorRepository.GetByIdAsync(id);
        if (sector == null)
            return NotFound();
        return sector;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Sector>>> GetAll()
    {
        var sectors = await _sectorRepository.GetAllAsync();
        return Ok(sectors);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, Sector sector)
    {
        if (id != sector.Id)
            return BadRequest();

        await _sectorRepository.UpdateAsync(sector);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var sector = await _sectorRepository.GetByIdAsync(id);
        if (sector == null)
            return NotFound();

        await _sectorRepository.DeleteAsync(sector);
        return NoContent();
    }
} 