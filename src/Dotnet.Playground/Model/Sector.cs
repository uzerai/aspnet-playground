using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Dotnet.Playground.Model.Organizations;

namespace Dotnet.Playground.Model;

public class Sector : BaseEntity
{
    public required string Name { get; set; }
    public required Guid CragId { get; set; }
    [ForeignKey("CragId")]
    public virtual Crag Crag { get; set; } = null!;
    public virtual ICollection<Route> Routes { get; set; } = [];
}
