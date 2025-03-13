using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Uzerai.Dotnet.Playground.Model.Authentication;
using Uzerai.Dotnet.Playground.Model.Tags;

namespace Uzerai.Dotnet.Playground.Model.Organizations;
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
    [JsonIgnore]
    public virtual ICollection<Tag> Tags { get; set; } = [];
}