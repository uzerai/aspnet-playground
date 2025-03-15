using System.ComponentModel.DataAnnotations.Schema;
using Dotnet.Playground.Model.Authentication;
using Dotnet.Playground.Model.Organizations;
using Dotnet.Playground.Model.Tags;

namespace Dotnet.Playground.Model;

[Table("documents")]
public partial class Document : Taggable
{
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required Guid OrganizationId { get; set; }
    public required Guid AuthorId { get; set; }

    [ForeignKey("OrganizationId")]
    public virtual Organization Organization { get; set; } = null!;
    [ForeignKey("AuthorId")]
    public virtual User Author { get; set; } = null!;
}