using NetTopologySuite.Geometries;

namespace Dotnet.Playground.DTO.RequestData;

public record AreaRequestData(
    string Name,
    string Description,
    Point Location,
    MultiPolygon Boundary
);
