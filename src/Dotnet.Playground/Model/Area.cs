using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Dotnet.Playground.Model.Media.Image;
using Dotnet.Playground.Model.Organizations;
using NetTopologySuite.Geometries;

namespace Dotnet.Playground.Model;

/// <summary>
/// Represents a climing area from a colloquial perspective.
/// 
/// The area can be a single face of a wall, or a scattered collection of boulders/walls/routes, and
/// therefore allows the definition of several polygonal boundaries as its boundary.
/// 
/// Most commonly represents a geographical area within which one or more climbing sectors are located.
/// </summary>
[Table("areas")]
public class Area : BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required Point Location { get; set; }
    public required MultiPolygon Boundary { get; init; }

    [JsonIgnore]
    [ForeignKey("MaintainerOrganization")]
    public Guid? MaintainerOrganizationId { get; set; }
    [JsonIgnore]
    public virtual Organization? MaintainerOrganization { get; set; }
    [JsonIgnore]
    public virtual ICollection<Sector> Sectors { get; set; } = [];
    public virtual ICollection<AreaImage> Images { get; set; } = [];
}
