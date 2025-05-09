using Dotnet.Playground.DI.Repository.Interface;
using Dotnet.Playground.DTO.RequestData;
using Dotnet.Playground.Model;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;

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
    public async Task<ActionResult<Area>> Create([FromBody] CreateAreaRequestData requestData)
    {
        // var createdArea = await _areaRepository.CreateAsync(new() {
        //     Name = requestData.Name,
        //     Description = requestData.Description,
        //     Location = new Point(requestData.Location.X, requestData.Location.Y, requestData.Location.Z),
        //     Boundary = new MultiPolygon(new[] {
        //         new Polygon(new LinearRing(requestData.Boundary.Append(requestData.Boundary[0]).ToArray()))
        //     })
        // });

        var createdArea = await _areaRepository.CreateAsync(new() {
            Name = requestData.Name,
            Description = requestData.Description,
            Location = requestData.Location,
            Boundary = requestData.Boundary
        });
        
        return Created(createdArea.Id.ToString(), createdArea);
    }

    [HttpGet("{areaId}")]
    public async Task<ActionResult<Area>> GetById([FromRoute] Guid areaId)
    {
        var area = await _areaRepository.GetByIdAsync(areaId);
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

    [HttpPut("{areaId}")]
    public async Task<IActionResult> Update([FromRoute] Guid areaId, [FromBody] Area area)
    {
        if (areaId != area.Id)
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