using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet.Playground.Model.Media.Image;

public class SectorImage : Image
{
    public required Guid SectorId { get; set; }
    public virtual Sector Sector { get; set; } = null!;
}
