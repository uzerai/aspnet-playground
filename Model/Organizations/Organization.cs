using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Uzerai.Dotnet.Playground.Model.Authentication;

namespace Uzerai.Dotnet.Playground.Model.Organizations;
public class Organization : BaseEntity
{
    [Required]
    public required string Name { get; set; }
    [JsonIgnore]
    public virtual ICollection<User> Users { get; set; } = [];
    [JsonIgnore]
    public virtual ICollection<OrganizationUser> OrganizationUsers { get; set; } = [];
}