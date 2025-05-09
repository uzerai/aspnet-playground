using Dotnet.Playground.Model;

namespace Dotnet.Playground.DTO.RequestData;

public record PitchRequestData(
    Guid SectorId,
    string Name,
    PitchType Type,
    string? Description,
    string? Grade);
