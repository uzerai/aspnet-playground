using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Uzerai.Dotnet.Playground.Model.Authentication;
using Uzerai.Dotnet.Playground.Model.Authorization.Permissions;

namespace Uzerai.Dotnet.Playground.Model.Organizations;

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
    public virtual IEnumerable<OrganizationPermission> Permissions { get; set; } = [];
}