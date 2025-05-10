using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet.Playground.Model.Media.Image;

public class PitchImage : Image
{
    public required Guid PitchId { get; set; }
    public virtual Pitch Pitch { get; set; } = null!;
}
