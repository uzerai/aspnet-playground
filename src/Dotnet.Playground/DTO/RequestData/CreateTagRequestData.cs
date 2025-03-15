using System.ComponentModel.DataAnnotations;

namespace Dotnet.Playground.DTO.RequestData;

public class CreateTagRequestData
{
    [Required]
    public required string Name { get; set; }
    [Required]
    public required string Description { get; set; }
    public string? Color { get; set; }
}