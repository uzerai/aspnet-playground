using System.ComponentModel.DataAnnotations.Schema;
using Dotnet.Playground.Model.Authentication;

namespace Dotnet.Playground.Model;

public class Note : BaseEntity
{
    public required string Title { get; set; }
    public required string Content { get; set; }

    public required Guid AuthorId { get; set; }
    [ForeignKey("AuthorId")]
    public virtual User Author { get; set; } = null!;
    public required Guid CragId { get; set; }
    [ForeignKey("CragId")]
    public virtual Crag Crag { get; set; } = null!;
}