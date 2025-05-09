using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet.Playground.Model;

/// <summary>
/// Represents a pitch of a climbing route.
/// 
/// A pitch in the context of the application, can be a rope-climbed pitch, or a boulder problem.
/// </summary>
public class Pitch : BaseEntity
{
    public required string Name { get; set; }
    public required PitchType Type { get; set; } = PitchType.Sport;
    public string? Description { get; set; }
    
    [ForeignKey("Sector")]
    public required Guid SectorId { get; set; }
    public virtual Sector Sector { get; set; } = null!;
    public virtual ICollection<Route> Routes { get; set; } = [];
}
