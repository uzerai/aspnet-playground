using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Uzerai.Dotnet.Playground.Model.Authorization.Permissions;

namespace Uzerai.Dotnet.Playground.DI.Authorization.Permissions;

public class JsonPermissionsConverter : JsonConverter<Permission>
{
    public override Permission Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        string? permissionString = reader.GetString();
        if (string.IsNullOrEmpty(permissionString))
            throw new JsonException("Permission string cannot be null or empty");

        // Split the string by colon (e.g., "users:read" -> ["users", "read"])
        string[] parts = permissionString.Split(':');
        if (parts.Length != 2)
            throw new JsonException($"Invalid permission format: {permissionString}");

        // Capitalize first letter of each part
        string resource = char.ToUpperInvariant(parts[0][0]) + parts[0].Substring(1);
        string action = char.ToUpperInvariant(parts[1][0]) + parts[1].Substring(1);

        // Combine into the enum name format (e.g., "UsersRead")
        string enumName = resource + action;

        // Try to parse as enum
        if (Enum.TryParse<Permission>(enumName, out var permission))
            return permission;

        throw new JsonException($"Invalid permission: {permissionString}");
    }

    public override void Write(
        Utf8JsonWriter writer,
        Permission permission,
        JsonSerializerOptions options)
    {
        // Convert enum name to string
        string permissionName = permission.ToString();

        // Use regex to find the resource and action parts
        string pattern = @"([A-Z][a-z]+)([A-Z][a-z]+)";
        Match match = Regex.Match(permissionName, pattern);

        Console.WriteLine(match.Groups[1].Value);
        Console.WriteLine(match.Groups[2].Value);

        if (!match.Success)
            throw new JsonException($"Cannot convert permission to string: {permissionName}");

        // Extract resource and action parts and convert to lowercase
        string resource = match.Groups[1].Value.ToLowerInvariant();
        string action = match.Groups[2].Value.ToLowerInvariant();

        // Format as "resource:action"
        writer.WriteStringValue($"{resource}:{action}");
    }
}