using NetTopologySuite.Geometries;

namespace Dotnet.Playground.DTO.RequestData;

public record CreateAreaRequestData(
    string Name,
    string Description,
    Point Location,
    MultiPolygon Boundary
);
