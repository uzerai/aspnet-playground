using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using Uzerai.Dotnet.Playground.DI.Authorization.ConfigurationExtension;
using Uzerai.Dotnet.Playground.Model.Authorization.Permissions;
using Uzerai.Dotnet.Playground.Model.Organizations;

namespace Uzerai.Dotnet.Playground.Model.Authentication;

[Index(nameof(Auth0UserId), IsUnique = true)]
[Index(nameof(Email))]
public class User : BaseEntity
{
    [Required]
    [Column("auth0_user_id")]
    public required string Auth0UserId { get; set; }

    [Required]
    public required string Email { get; set; }

    [Required]
    public required string Username { get; set; }

    [Required]
    public Instant LastLogin { get; set; }

    // OrganizationUser relationships
    [Column("platform_permissions", TypeName = "jsonb")]
    public virtual ICollection<Permission> PlatformPermissions { get; set; } = [];
    public virtual ICollection<OrganizationUser> OrganizationUsers { get; set; } = [];

    public virtual ICollection<Organization> Organizations { get; set; } = [];
}