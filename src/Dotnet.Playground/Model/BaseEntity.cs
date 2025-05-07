using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace Dotnet.Playground.Model;

[PrimaryKey(nameof(Id))]
public abstract partial class BaseEntity
{
    public Guid Id { get; set; }

    [Required]
    public Instant CreatedAt { get; set; }
    [Required]
    public Instant UpdatedAt { get; set; }

    [JsonIgnore]
    public Instant? DeletedAt { get; set; }
}
