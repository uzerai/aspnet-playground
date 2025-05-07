using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Playground.Model;

[PrimaryKey(nameof(RouteId), nameof(PitchId))]
public class RoutePitch
{
    [ForeignKey("Route")]
    public required Guid RouteId { get; set; }
    [ForeignKey("Pitch")]
    public required Guid PitchId { get; set; }
    public required int PitchNumber { get; set; }

    public virtual Route Route { get; set; } = null!;
    public virtual Pitch Pitch { get; set; } = null!;
}