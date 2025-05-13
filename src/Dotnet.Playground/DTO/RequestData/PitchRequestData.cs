using Dotnet.Playground.Model.Location;

namespace Dotnet.Playground.DTO.RequestData;

public record PitchRequestData(
    Guid SectorId,
    string Name,
    PitchType Type,
    string? Description,
    string? Grade);
