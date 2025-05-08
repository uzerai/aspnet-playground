using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Dotnet.Playground.Model.Organizations;

namespace Dotnet.Playground.Model;

[Table("areas")]
public class Area : BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string Location { get; set; }

    [JsonIgnore]
    [ForeignKey("MaintainerOrganization")]
    public Guid MaintainerOrganizationId { get; set; }
    [JsonIgnore]
    public virtual Organization MaintainerOrganization { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Sector> Sectors { get; set; } = [];
}
