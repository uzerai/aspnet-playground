using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NodaTime;

namespace Playground.Model.Authentication;

public class User : BaseEntity
{
    [Required]
    [Column("auth0_user_id")]
    public required string Auth0UserId { get; set; }

    [Required]
    public required string Email { get; set; }

    [Required]
    public required string Username { get; set; }

    [Required]
    public Instant LastLogin { get; set; }
}