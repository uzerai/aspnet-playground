using Dotnet.Playground.DI.Repository.Interface;
using Dotnet.Playground.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Playground.Controllers;

[ApiController]
[Route("crags")]
public class CragController : ControllerBase
{
    private readonly IEntityRepository<Crag> _cragRepository;

    public CragController(IEntityRepository<Crag> cragRepository)
    {
        _cragRepository = cragRepository;
    }

    [HttpPost]
    public async Task<ActionResult<Crag>> Create(Crag crag)
    {
        var createdCrag = await _cragRepository.CreateAsync(crag);
        return CreatedAtAction(nameof(GetById), new { id = createdCrag.Id }, createdCrag);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Crag>> GetById(Guid id)
    {
        var crag = await _cragRepository.GetByIdAsync(id);
        if (crag == null)
            return NotFound();
        return crag;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Crag>>> GetAll()
    {
        var crags = await _cragRepository.GetAllAsync();
        return Ok(crags);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, Crag crag)
    {
        if (id != crag.Id)
            return BadRequest();

        await _cragRepository.UpdateAsync(crag);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var crag = await _cragRepository.GetByIdAsync(id);
        if (crag == null)
            return NotFound();

        await _cragRepository.DeleteAsync(crag);
        return NoContent();
    }
} 