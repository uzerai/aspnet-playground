using System.Text.Json.Serialization;

namespace Dotnet.Playground.Model;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PitchType
{
    [JsonStringEnumMemberName("sport")]
    Sport,
    [JsonStringEnumMemberName("traditional")]
    Traditional,
    [JsonStringEnumMemberName("mixed")]
    Mixed,
    [JsonStringEnumMemberName("aid")]
    Aid,
    [JsonStringEnumMemberName("boulder")]
    Boulder
}