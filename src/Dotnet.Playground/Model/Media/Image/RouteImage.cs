using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet.Playground.Model.Media.Image;

public class RouteImage : Image
{
    public required Guid RouteId { get; set; }
    public virtual Route Route { get; set; } = null!;
}
