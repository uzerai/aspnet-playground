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
[Route("organizations/{organizationId}/teams/{teamId}/users")]
[ApiController]
public class OrganizationTeamUsersController : ControllerBase
{
    private readonly OrganizationTeamUserRepository _organizationTeamUserRepository;

    public OrganizationTeamUsersController(OrganizationTeamUserRepository organizationTeamUserRepository)
    {
        _organizationTeamUserRepository = organizationTeamUserRepository;
    }

    [HttpPost]
    [OrganizationPermissionRequired(Permission.TeamsWrite)]
    public async Task<IActionResult> Create(
        [FromRoute][Required] Guid organizationId,
        [FromRoute][Required] Guid teamId,
        [FromBody][Required] CreateOrganizationTeamUserRequestData requestData)
    {
        var organizationTeamUser = new OrganizationTeamUser
        {
            OrganizationId = organizationId,
            OrganizationTeamId = teamId,
            UserId = requestData.UserId,
            Permissions = requestData.Permissions,
        };

        await _organizationTeamUserRepository.CreateAsync(organizationTeamUser);

        return Ok(organizationTeamUser);
    }
}
