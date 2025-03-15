using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dotnet.Playground.DI.Authorization.Permissions;
using Dotnet.Playground.DI.Repository.Interface;
using Dotnet.Playground.DTO.RequestData;
using Dotnet.Playground.Model.Authorization.Permissions;
using Dotnet.Playground.Model.Organizations;

namespace Dotnet.Playground.Controllers;

[Authorize]
[Route("organizations/{organizationId}/teams")]
[ApiController]
public class OrganizationTeamsController : ControllerBase
{
    private readonly IEntityRepository<OrganizationTeam> _organizationTeamRepository;

    public OrganizationTeamsController(IEntityRepository<OrganizationTeam> organizationTeamRepository)
    {
        _organizationTeamRepository = organizationTeamRepository;
    }

    [HttpGet]
    [OrganizationPermissionRequired(Permission.TeamsRead)]
    public async Task<IActionResult> GetAll([FromRoute][Required] Guid organizationId)
    {
        var teams = await _organizationTeamRepository.BuildReadonlyQuery()
            .Where(e => e.OrganizationId == organizationId)
            .ToListAsync();
        return Ok(teams);
    }

    [HttpPost]
    [OrganizationPermissionRequired(Permission.TeamsWrite)]
    public async Task<IActionResult> Create(
      [FromRoute][Required] Guid organizationId,
      [FromBody][Required] CreateOrganizationTeamRequestData requestData)
    {
        var team = new OrganizationTeam
        {
            Name = requestData.Name,
            OrganizationId = organizationId,
        };

        await _organizationTeamRepository.CreateAsync(team);

        return Ok(team);
    }
}