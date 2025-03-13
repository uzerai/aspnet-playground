using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Uzerai.Dotnet.Playground.Model.Authentication;
using Uzerai.Dotnet.Playground.Model.Organizations;

namespace Uzerai.Dotnet.Playground.Model.Tags;

public class Tag : BaseEntity
{
    [Required]
    public required string Name { get; set; }
    [Required]
    public required string Description { get; set; }
    // Default color is Battleship Gray; #949B96
    public required string Color { get; set; } = "#949B96";
    [Required]
    [ForeignKey("CreatedById")]
    public required Guid CreatedById { get; set; }
    [Required]
    [ForeignKey("Organization")]
    public required Guid OrganizationId { get; set; }

    public virtual User CreatedBy { get; set; } = null!;
    public virtual Organization Organization { get; set; } = null!;
    public virtual ICollection<Taggable> TaggedEntities { get; set; } = [];
}
