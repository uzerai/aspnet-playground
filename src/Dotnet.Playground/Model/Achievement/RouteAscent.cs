using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet.Playground.Model.Achievement;

public class RouteAscent : BaseAscent
{
  [ForeignKey("Route")]
  public required Guid RouteId { get; set; }
  public virtual Route Route { get; set; } = null!;
}

