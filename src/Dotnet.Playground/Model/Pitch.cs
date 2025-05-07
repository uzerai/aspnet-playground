using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet.Playground.Model;

public class Pitch : BaseEntity
{
    public required string Name { get; set; }
    public required PitchType Type { get; set; } = PitchType.Sport;
    public required string Description { get; set; }
    
    public required Guid SectorId { get; set; }
    [ForeignKey("SectorId")]
    public virtual Sector Sector { get; set; } = null!;
    public virtual ICollection<Route> Routes { get; set; } = [];
}
