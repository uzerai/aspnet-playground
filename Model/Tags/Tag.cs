using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Uzerai.Dotnet.Playground.Model.Authentication;
using Uzerai.Dotnet.Playground.Model.Organizations;

namespace Uzerai.Dotnet.Playground.Model;

public class Tag : BaseEntity
{
    [Required]
    public required string Name { get; set; }
    [Required]
    public required string Description { get; set; }
    // Default color is Battleship Gray; #949B96
    public required string Color { get; set; } = "#949B96";
    [Required]
    public required Guid CreatedById { get; set; }
    [Required]
    public required Guid OrganizationId { get; set; }

    [ForeignKey("CreatedById")]
    public required User CreatedBy { get; set; }
    [ForeignKey("OrganizationId")]
    public virtual Organization Organization { get; set; } = null!;
    public virtual ICollection<TaggedEntity> TaggedEntities { get; set; } = [];
}
