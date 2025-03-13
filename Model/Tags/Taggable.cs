namespace Uzerai.Dotnet.Playground.Model.Tags;

public abstract partial class Taggable : BaseEntity
{
    public virtual ICollection<Tag> Tags { get; set; } = [];
}
