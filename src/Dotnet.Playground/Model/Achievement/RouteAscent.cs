using System.ComponentModel.DataAnnotations.Schema;
using Route = Dotnet.Playground.Model.Location.Route;

namespace Dotnet.Playground.Model.Achievement;

public class RouteAscent : BaseAscent
{
  [ForeignKey("Route")]
  public required Guid RouteId { get; set; }
  public virtual Route Route { get; set; } = null!;
}

