namespace Dotnet.Playground.DTO.RequestData;

public record RouteRequestData(
    Guid SectorId,
    Guid[] PitchIds,
    string Name,
    string? Description,
    string? Grade,
    string? FirstAscentClimberName,
    string? BolterName);
