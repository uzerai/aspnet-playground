using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Dotnet.Playground.DI;
using Dotnet.Playground.DI.Authorization.Permissions;
using Dotnet.Playground.DI.Repository.Interface;
using Dotnet.Playground.DTO.RequestData;
using Dotnet.Playground.Model.Authorization.Permissions;
using Dotnet.Playground.Model.Tags;

namespace Dotnet.Playground.Controllers;

[Route("organizations/{organizationId}/tags")]
[ApiController]
public class TagsController : ControllerBase
{
  private readonly IEntityRepository<Tag> _tagRepository;

  public TagsController(IEntityRepository<Tag> tagRepository)
  {
    _tagRepository = tagRepository;
  }

  [HttpPost]
  [OrganizationPermissionRequired(Permission.TagsWrite)]
  public async Task<IActionResult> Create(
    [FromRoute][Required] Guid organizationId, 
    [FromBody][Required] CreateTagRequestData requestData)
  {
    var tag = new Tag
    {
      Name = requestData.Name,
      Description = requestData.Description,
      Color = requestData.Color ?? "#949B96",
      OrganizationId = organizationId,
      CreatedById = HttpContext.GetLocalUser().Id,
    };

    await _tagRepository.CreateAsync(tag);

    return Ok(tag);
  }
}