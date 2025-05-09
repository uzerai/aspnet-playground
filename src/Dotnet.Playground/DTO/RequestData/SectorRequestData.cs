using NetTopologySuite.Geometries;

namespace Dotnet.Playground.DTO.RequestData;

public record SectorRequestData(
    string Name,
    string Description,
    Point Location,
    MultiPolygon Boundary);