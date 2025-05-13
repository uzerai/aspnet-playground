using System.ComponentModel.DataAnnotations.Schema;
using Dotnet.Playground.Model.Location;

namespace Dotnet.Playground.Model.Achievement;

public class PitchAscent : BaseAscent
{
  [ForeignKey("Pitch")]
  public required Guid PitchId { get; set; }
  public virtual Pitch Pitch { get; set; } = null!;
}