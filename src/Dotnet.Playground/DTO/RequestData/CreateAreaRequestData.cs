using NetTopologySuite.Geometries;

namespace Dotnet.Playground.DTO.RequestData;

public class CreateAreaRequestData
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required Point Location { get; set; }
    public required MultiPolygon Boundary { get; set; }
}
