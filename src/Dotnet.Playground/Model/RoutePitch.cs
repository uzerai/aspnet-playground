using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Playground.Model;

[PrimaryKey(nameof(RouteId), nameof(PitchId))]
public class RoutePitch
{
    public required Guid RouteId { get; set; }
    public required Guid PitchId { get; set; }
    public required int PitchNumber { get; set; }

    [ForeignKey("RouteId")]
    public virtual Route Route { get; set; } = null!;
    [ForeignKey("PitchId")]
    public virtual Pitch Pitch { get; set; } = null!;
}