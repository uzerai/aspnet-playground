using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NodaTime;
using Uzerai.Dotnet.Playground.Controllers.CreateModel;
using Uzerai.Dotnet.Playground.DI.Repository;
using Uzerai.Dotnet.Playground.Model.Organization;

namespace Uzerai.Dotnet.Playground.Controllers;

[Authorize]
[Route("organizations")]
[ApiController]
public class OrganizationController : ControllerBase
{
    private readonly OrganizationRepository _organizationRepository;
    private readonly IClock _clock;

    public OrganizationController(OrganizationRepository organizationRepository, IClock clock)
    {
        _organizationRepository = organizationRepository;
        _clock = clock;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _organizationRepository.GetAllAsync());
    }

    [HttpGet("{organizationId}/")]
    public async Task<IActionResult> Get(Guid organizationId)
    {
        return Ok(await _organizationRepository.GetByIdAsync(organizationId));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrganizationRequestData createOrganizationRequest)
    {
        var organization = new Organization
        {
            Name = createOrganizationRequest.Name
        };

        return Ok(await _organizationRepository.CreateAsync(organization));
    }
}
