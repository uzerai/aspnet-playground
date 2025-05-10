using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Dotnet.Playground.Model.Authentication;
using Dotnet.Playground.Model.Authorization.Permissions;

namespace Dotnet.Playground.Model.Organizations;

[PrimaryKey(nameof(OrganizationId), nameof(UserId))]
public class OrganizationUser
{
    [Required]
    [ForeignKey("Organization")]
    public required Guid OrganizationId { get; set; }
    [Required]
    [ForeignKey("User")]
    public required Guid UserId { get; set; }
    [JsonIgnore]
    public virtual Organization Organization { get; set; } = null!;
    [JsonIgnore]
    public virtual User User { get; set; } = null!;

    // This could be a jsonb column, let's explore that later when we have entities that require filtering by permission.
    public virtual ICollection<OrganizationPermission> Permissions { get; set; } = DefaultUserPermissions.OrganizationPermissions.Select(x => new OrganizationPermission { Permission = x }).ToList();
    public virtual ICollection<OrganizationTeamUser> OrganizationTeamUsers { get; set; } = [];
}