using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet.Playground.Model;

public class Sector : BaseEntity
{
    public required string Name { get; set; }
    [ForeignKey("Area")]
    public required Guid AreaId { get; set; }
    public virtual Area Area { get; set; } = null!;
    public virtual ICollection<Route> Routes { get; set; } = [];
}
