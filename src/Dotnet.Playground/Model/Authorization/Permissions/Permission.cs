using System.Text.Json.Serialization;

namespace Dotnet.Playground.Model.Authorization.Permissions;

/// <summary>
/// Default permissions for the platform.
/// 
/// These permissions are the default permissions given to a user when they sign up.
/// </summary>
public static class DefaultUserPermissions
{
    public static readonly Permission[] PlatformPermissions = [
        Permission.OrganizationsRead,
        Permission.AreasRead,
        Permission.SectorsRead,
        Permission.RoutesRead,
        Permission.PitchesRead,
    ];

    public static readonly Permission[] OrganizationPermissions = [
        Permission.TeamsRead,
        Permission.OrganizationsRead,
        Permission.UsersRead,
    ];
    public static readonly Permission[] TeamPermissions = [];
    public static readonly Permission[] AdminPermissions = [];
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Permission
{
    [JsonStringEnumMemberName("users:read")]
    UsersRead,
    [JsonStringEnumMemberName("users:write")]
    UsersWrite,
    [JsonStringEnumMemberName("organizations:read")]
    OrganizationsRead,
    [JsonStringEnumMemberName("organizations:write")]
    OrganizationsWrite,
    [JsonStringEnumMemberName("permissions:read")]
    PermissionsRead,
    [JsonStringEnumMemberName("permissions:write")]
    PermissionsWrite,
    [JsonStringEnumMemberName("teams:read")]
    TeamsRead,
    [JsonStringEnumMemberName("teams:write")]
    TeamsWrite,
    [JsonStringEnumMemberName("areas:read")]
    AreasRead,
    [JsonStringEnumMemberName("areas:write")]
    AreasWrite,
    [JsonStringEnumMemberName("routes:read")]
    RoutesRead,
    [JsonStringEnumMemberName("routes:write")]
    RoutesWrite,
    [JsonStringEnumMemberName("pitches:read")]
    PitchesRead,
    [JsonStringEnumMemberName("pitches:write")]
    PitchesWrite,
    [JsonStringEnumMemberName("sectors:read")]
    SectorsRead,
    [JsonStringEnumMemberName("sectors:write")]
    SectorsWrite,
    [JsonStringEnumMemberName("images:read")]
    ImagesRead,
    [JsonStringEnumMemberName("images:write")]
    ImagesWrite
}