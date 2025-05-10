using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet.Playground.Model.Media.Image;

public class AreaImage : Image
{
    public required Guid AreaId { get; set; }
    public virtual Area Area { get; set; } = null!;
}
