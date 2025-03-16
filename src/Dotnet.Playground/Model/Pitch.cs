using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet.Playground.Model;

public class Pitch : BaseEntity
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    
    public required Guid CragId { get; set; }
    [ForeignKey("CragId")]
    public virtual Crag Crag { get; set; } = null!;
    public virtual ICollection<Route> Routes { get; set; } = [];
}
