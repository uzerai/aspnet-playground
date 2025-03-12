using System.ComponentModel.DataAnnotations;

namespace Uzerai.Dotnet.Playground.Model.Organizations;

public class OrganizationTeam : BaseEntity
{
    [Required]
    public required string Name { get; set; }
    public Guid OrganizationId { get; set; }  
    public virtual Organization Organization { get; set; } = null!;
    
}
