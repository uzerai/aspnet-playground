using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Uzerai.Dotnet.Playground.Model.Authentication;

namespace Uzerai.Dotnet.Playground.Model.Organizations;

[PrimaryKey(nameof(OrganizationId), nameof(OrganizationTeamId), nameof(UserId))]
public class OrganizationTeamUser
{
    [Required]
    [ForeignKey("Organization")]
    public Guid OrganizationId { get; set; }
    [Required]
    public Guid UserId { get; set; }
    [Required]
    [ForeignKey("OrganizationTeam")]
    public Guid OrganizationTeamId { get; set; }
    
    public virtual User User { get; set; } = null!;
    public virtual Organization Organization { get; set; } = null!;
    public virtual OrganizationUser OrganizationUser { get; set; } = null!;
    public virtual OrganizationTeam OrganizationTeam { get; set; } = null!;
}