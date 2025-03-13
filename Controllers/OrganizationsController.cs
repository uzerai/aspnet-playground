using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NodaTime;
using Uzerai.Dotnet.Playground.Controllers.CreateModel;
using Uzerai.Dotnet.Playground.DI;
using Uzerai.Dotnet.Playground.DI.Repository;
using Uzerai.Dotnet.Playground.Model.Authorization.Permissions;
using Uzerai.Dotnet.Playground.Model.Organizations;

namespace Uzerai.Dotnet.Playground.Controllers;

[Authorize]
[Route("organizations")]
[ApiController]
public class OrganizationsController : ControllerBase
{
    private readonly OrganizationRepository _organizationRepository;
    private readonly OrganizationUserRepository _organizationUserRepository;

    public OrganizationsController(OrganizationRepository organizationRepository, OrganizationUserRepository organizationUserRepository)
    {
        _organizationRepository = organizationRepository;
        _organizationUserRepository = organizationUserRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _organizationRepository.GetAllAsync());
    }

    [HttpGet("{organizationId}/")]
    public async Task<IActionResult> Get([FromRoute][Required] Guid organizationId)
    {
        return Ok(await _organizationRepository.GetByIdAsync(organizationId));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrganizationRequestData createOrganizationRequest)
    {
        var newOrganization = new Organization
        {
            Name = createOrganizationRequest.Name,
        };

        var createdOrganization = await _organizationRepository.CreateAsync(newOrganization);
        var organizationUser = new OrganizationUser
        {
            OrganizationId = createdOrganization.Id,
            UserId = HttpContext.GetLocalUser().Id,
            Permissions = new List<OrganizationPermission>
            {
                new OrganizationPermission
                {
                    Permission = Permission.UsersRead
                },
                new OrganizationPermission
                {
                    Permission = Permission.UsersWrite
                },
                new OrganizationPermission
                {
                    Permission = Permission.OrganizationsRead
                },
                new OrganizationPermission
                {
                    Permission = Permission.OrganizationsWrite
                }
            },
        };

        await _organizationUserRepository.CreateAsync(organizationUser);

        return Ok(createdOrganization);
    }
}
