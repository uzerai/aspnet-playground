using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Dotnet.Playground.Model.Authentication;
using Dotnet.Playground.Model.Authorization.Permissions;

namespace Dotnet.Playground.Model.Organizations;

[PrimaryKey(nameof(OrganizationId), nameof(OrganizationTeamId), nameof(UserId))]
public class OrganizationTeamUser
{
    [Required]
    [ForeignKey("Organization")]
    public Guid OrganizationId { get; set; }
    [Required]
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    [Required]
    [ForeignKey("OrganizationTeam")]
    public Guid OrganizationTeamId { get; set; }
    [Column("permissions", TypeName = "jsonb")]
    public virtual ICollection<Permission> Permissions { get; set; } = [ Permission.TeamsRead ];

    public virtual User User { get; set; } = null!;
    public virtual Organization Organization { get; set; } = null!;
    public virtual OrganizationUser OrganizationUser { get; set; } = null!;
    public virtual OrganizationTeam OrganizationTeam { get; set; } = null!;
}