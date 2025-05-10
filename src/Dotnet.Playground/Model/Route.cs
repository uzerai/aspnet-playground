using System.ComponentModel.DataAnnotations.Schema;
using Dotnet.Playground.Model.Media.Image;
using NodaTime;

namespace Dotnet.Playground.Model;

/// <summary>
/// Represents a climbing route; from top to bottom.
/// 
/// A route must contain at least one pitch.
/// </summary>
public class Route : BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Grade { get; set; }
    public Instant? FirstAscentDate { get; set; }
    public string? FirstAscentClimberName { get; set; }
    public string? BolterName { get; set; }

    [ForeignKey("Sector")]
    public required Guid SectorId { get; set; }
    public virtual Sector Sector { get; set; } = null!;

    [ForeignKey("TopoImage")]
    public Guid? TopoImageId { get; set; }
    public virtual RouteImage? TopoImage { get; set; }
    
    public virtual ICollection<Pitch> Pitches { get; set; } = [];
}