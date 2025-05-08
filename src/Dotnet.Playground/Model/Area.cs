using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Dotnet.Playground.Converters;
using Dotnet.Playground.Model.Organizations;
using NetTopologySuite.Geometries;

namespace Dotnet.Playground.Model;

[Table("areas")]
public class Area : BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required Point Location { get; set; }
    public required MultiPolygon Boundary { get; set; }

    [JsonIgnore]
    [ForeignKey("MaintainerOrganization")]
    public Guid? MaintainerOrganizationId { get; set; }
    [JsonIgnore]
    public virtual Organization? MaintainerOrganization { get; set; }
    [JsonIgnore]
    public virtual ICollection<Sector> Sectors { get; set; } = [];
}
