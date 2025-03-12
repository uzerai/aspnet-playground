using System.ComponentModel.DataAnnotations;
using NodaTime;

namespace Uzerai.Dotnet.Playground.Model;

public abstract class BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Instant CreatedAt { get; set; }

    [Required]
    public Instant UpdatedAt { get; set; }

    public Instant? DeletedAt { get; set; }
}
