using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Uzerai.Dotnet.Playground.Model.Authentication;
using Uzerai.Dotnet.Playground.Model.Authorization.Permissions;

namespace Uzerai.Dotnet.Playground.Model.Organizations;

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
    public virtual ICollection<OrganizationPermission> Permissions { get; set; } = [];
    public virtual ICollection<OrganizationTeamUser> OrganizationTeamUsers { get; set; } = [];
}