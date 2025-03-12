using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Uzerai.Dotnet.Playground.DI.Authorization.Permissions;
using Uzerai.Dotnet.Playground.DI.Repository;
using Uzerai.Dotnet.Playground.DTO.RequestData;
using Uzerai.Dotnet.Playground.Model.Authorization.Permissions;
using Uzerai.Dotnet.Playground.Model.Organizations;

namespace Uzerai.Dotnet.Playground.Controllers;

[Authorize]
[Route("organizations/{organizationId}/teams")]
[ApiController]
public class OrganizationTeamsController : ControllerBase
{
    private readonly OrganizationTeamRepository _organizationTeamRepository;

    public OrganizationTeamsController(OrganizationTeamRepository organizationTeamRepository)
    {
        _organizationTeamRepository = organizationTeamRepository;
    }
    
    [HttpGet]
    [OrganizationPermissionRequired(Permission.TeamsRead)]
    public async Task<IActionResult> GetAll([FromRoute][Required]Guid organizationId)
    {
        var teams = await _organizationTeamRepository.GetAllForOrganizationAsync(organizationId);
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