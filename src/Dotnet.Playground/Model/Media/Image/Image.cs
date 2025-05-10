using System.ComponentModel.DataAnnotations.Schema;
using Dotnet.Playground.Model.Authentication;

namespace Dotnet.Playground.Model.Media.Image;

public abstract partial class Image : BaseEntity
{
    public required string Key { get; set; }
    public required string Bucket { get; set; }
    public required Uri Url { get; set; }
    public string? Description { get; set; }

    [ForeignKey("Uploader")]
    public required Guid UploaderId { get; set; }
    public virtual User Uploader { get; set; } = null!;
}
