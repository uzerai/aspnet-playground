using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Uzerai.Dotnet.Playground.Model.Authentication;

namespace Uzerai.Dotnet.Playground.Model.Organization;
public class Organization : BaseEntity
{
    [Required]
    public required string Name { get; set; }
    [JsonIgnore]
    public virtual IEnumerable<User> Users { get; set; } = [];
}