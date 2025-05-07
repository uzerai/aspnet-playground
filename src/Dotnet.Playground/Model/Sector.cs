using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet.Playground.Model;

public class Sector : BaseEntity
{
    public required string Name { get; set; }
    [ForeignKey("Crag")]
    public required Guid CragId { get; set; }
    public virtual Crag Crag { get; set; } = null!;
    public virtual ICollection<Route> Routes { get; set; } = [];
}
