using System.ComponentModel.DataAnnotations;
using NodaTime;

namespace Playground.Model.Authentication;

public class User : BaseEntity
{
    [Required]
    public required string Auth0UserId { get; set; }

    [Required]
    public required string Email { get; set; }

    [Required]
    public required string Username { get; set; }

    [Required]
    public Instant LastLogin { get; set; }
}