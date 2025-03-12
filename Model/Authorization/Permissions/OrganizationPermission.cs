using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Uzerai.Dotnet.Playground.Model.Organizations;

namespace Uzerai.Dotnet.Playground.Model.Authorization.Permissions;

[PrimaryKey(nameof(OrganizationId), nameof(UserId), nameof(Permission))]
public class OrganizationPermission {
  public Permission Permission { get; set; }
  [JsonIgnore]
  public Guid OrganizationId { get; set; }
  [JsonIgnore]
  public Guid UserId { get; set; }
  [JsonIgnore]
  public virtual OrganizationUser OrganizationUser { get; set; } = null!;
}