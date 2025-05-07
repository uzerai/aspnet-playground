using System.ComponentModel.DataAnnotations.Schema;
using NodaTime;

namespace Dotnet.Playground.Model;

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
    public virtual ICollection<Pitch> Pitches { get; set; } = [];
}