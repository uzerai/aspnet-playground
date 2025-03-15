using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Dotnet.Playground.DI.Authorization.Permissions;
using Dotnet.Playground.DI.Repository;
using Dotnet.Playground.DI.Repository.Interface;
using Dotnet.Playground.DTO.RequestData;
using Dotnet.Playground.Model.Authorization.Permissions;
using Dotnet.Playground.Model.Organizations;

namespace Dotnet.Playground.Controllers;

[Authorize]
[Route("organizations/{organizationId}/teams/{teamId}/users")]
[ApiController]
public class OrganizationTeamUsersController : ControllerBase
{
    private readonly IRepository<OrganizationTeamUser> _organizationTeamUserRepository;

    public OrganizationTeamUsersController(IRepository<OrganizationTeamUser> organizationTeamUserRepository)
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
