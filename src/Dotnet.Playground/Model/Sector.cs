using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;

namespace Dotnet.Playground.Model;

/// <summary>
/// Represents a sector of a climbing area; most commonly a single face of a wall,
/// or an area suitable to belay one or more routes.
/// </summary>
public class Sector : BaseEntity
{
    public required string Name { get; set; }

    // Geospatial data for the sector
    public required Polygon SectorArea { get; set; }
    public required Point EntryPoint { get; set; }
    public Point? RecommendedParkingLocation { get; set; }
    public LineString? Route { get; set; }
    [ForeignKey("Area")]
    public required Guid AreaId { get; set; }
    public virtual Area Area { get; set; } = null!;
    public virtual ICollection<Route> Routes { get; set; } = [];
}
