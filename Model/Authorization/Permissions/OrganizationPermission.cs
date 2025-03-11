using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Uzerai.Dotnet.Playground.Model.Organization;

namespace Uzerai.Dotnet.Playground.Model.Authorization.Permissions;

[PrimaryKey(nameof(OrganizationUserId), nameof(Permission))]
public class OrganizationPermission {
  public Permission Permission { get; set; }
  public required string OrganizationUserId { get; set; }
  public virtual OrganizationUser OrganizationUser { get; set; } = null!;
}