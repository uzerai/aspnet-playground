using NetTopologySuite.Geometries;

namespace Dotnet.Playground.DTO.RequestData;

public record SectorRequestData(
    string Name,
    Polygon SectorArea,
    Point EntryPoint,
    Point? RecommendedParkingLocation,
    LineString? ApproachPath,
    Guid AreaId);