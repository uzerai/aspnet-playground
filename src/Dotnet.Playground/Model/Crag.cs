using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Dotnet.Playground.Model.Organizations;

namespace Dotnet.Playground.Model;

public class Crag : BaseEntity
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Location { get; set; }
    public string? HowToGetThere { get; set; }

    [JsonIgnore]
    public required Guid MaintainerOrganizationId { get; set; }
    [ForeignKey("MaintainerOrganizationId")]
    public virtual Organization MaintainerOrganization { get; set; } = null!;
    public virtual ICollection<Sector> Sectors { get; set; } = [];
}
