using Dotnet.Playground.Model.Authentication;
using NodaTime;

namespace Dotnet.Playground.Model.Achievement;

/// <summary>
/// Represents a user's ascent of a route/boulder/pitch.
/// </summary>
public abstract partial class BaseAscent
{
  public required Guid Id { get; set; }
  public required Instant CompletedAt { get; set; }
  public required Instant CreatedAt { get; set; }
  public required Guid UserId { get; set; }
  public virtual User User { get; set; } = null!;
}