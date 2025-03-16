using System.ComponentModel.DataAnnotations.Schema;
using NodaTime;

namespace Dotnet.Playground.Model;

public class Route : BaseEntity
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Grade { get; set; }
    public Instant? FirstAscentDate { get; set; }
    public string? FirstAscentClimberName { get; set; }
    public string? BolterName { get; set; }
    public required Guid CragId { get; set; }
    [ForeignKey("CragId")]
    public virtual Crag Crag { get; set; } = null!;
    public virtual ICollection<Pitch> Pitches { get; set; } = [];
}