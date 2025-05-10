using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Dotnet.Playground.Model.Authentication;

namespace Dotnet.Playground.Model.Organizations;
public class Organization : BaseEntity
{
    [Required]
    public required string Name { get; set; }
    [JsonIgnore]
    public virtual ICollection<User> Users { get; set; } = [];
    [JsonIgnore]
    public virtual ICollection<OrganizationUser> OrganizationUsers { get; set; } = [];
    [JsonIgnore]
    public virtual ICollection<OrganizationTeam> Teams { get; set; } = [];
}