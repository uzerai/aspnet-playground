using NetTopologySuite.Geometries;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Dotnet.Playground.DTO.RequestData;

public class CreateAreaRequestData
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required CoordinateZ Location { get; set; }
    public required CoordinateZ[] Boundary { get; set; }
}
