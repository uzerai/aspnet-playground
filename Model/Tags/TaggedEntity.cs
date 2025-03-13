using System.ComponentModel.DataAnnotations;
using NodaTime;

namespace Uzerai.Dotnet.Playground.Model;

public abstract class TaggedEntity : BaseEntity
{
    public virtual ICollection<Tag> Tags { get; set; } = [];
}
