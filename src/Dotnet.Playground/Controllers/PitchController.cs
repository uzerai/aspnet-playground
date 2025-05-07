using Dotnet.Playground.DI.Repository.Interface;
using Dotnet.Playground.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Playground.Controllers;

[ApiController]
[Route("pitches")]
public class PitchController : ControllerBase
{
    private readonly IEntityRepository<Pitch> _pitchRepository;

    public PitchController(IEntityRepository<Pitch> pitchRepository)
    {
        _pitchRepository = pitchRepository;
    }

    [HttpPost]
    public async Task<ActionResult<Pitch>> Create(Pitch pitch)
    {
        var createdPitch = await _pitchRepository.CreateAsync(pitch);
        return CreatedAtAction(nameof(GetById), new { id = createdPitch.Id }, createdPitch);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Pitch>> GetById(Guid id)
    {
        var pitch = await _pitchRepository.GetByIdAsync(id);
        if (pitch == null)
            return NotFound();
        return pitch;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pitch>>> GetAll()
    {
        var pitches = await _pitchRepository.GetAllAsync();
        return Ok(pitches);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, Pitch pitch)
    {
        if (id != pitch.Id)
            return BadRequest();

        await _pitchRepository.UpdateAsync(pitch);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var pitch = await _pitchRepository.GetByIdAsync(id);
        if (pitch == null)
            return NotFound();

        await _pitchRepository.DeleteAsync(pitch);
        return NoContent();
    }
} 