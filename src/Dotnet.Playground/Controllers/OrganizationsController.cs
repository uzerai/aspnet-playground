using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Dotnet.Playground.DI;
using Dotnet.Playground.DI.Authorization.Permissions;
using Dotnet.Playground.DI.Repository.Interface;
using Dotnet.Playground.Model.Authorization.Permissions;
using Dotnet.Playground.Model.Organizations;
using Dotnet.Playground.DTO.RequestData.Organizations;
namespace Dotnet.Playground.Controllers;

[Authorize]
[Route("organizations")]
[ApiController]
public class OrganizationsController : ControllerBase
{
    private readonly IEntityRepository<Organization> _organizationRepository;
    private readonly IRepository<OrganizationUser> _organizationUserRepository;
    private readonly ILogger<OrganizationsController> _logger;

    public OrganizationsController(IEntityRepository<Organization> organizationRepository, IRepository<OrganizationUser> organizationUserRepository, ILogger<OrganizationsController> logger)
    {
        _organizationRepository = organizationRepository;
        _organizationUserRepository = organizationUserRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {   
        return Ok(await _organizationRepository.GetAllAsync());
    }

    [HttpGet("{organizationId}")]
    [OrganizationPermissionRequired(Permission.OrganizationsRead)]
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
            Permissions = Enum.GetValues<Permission>()
                .Select(permission => new OrganizationPermission
                {
                    Permission = permission
                }).ToList()
        };

        await _organizationUserRepository.CreateAsync(organizationUser);

        return Ok(createdOrganization);
    }
}
