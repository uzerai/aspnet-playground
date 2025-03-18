using System.Text.Json.Serialization;

namespace Dotnet.Playground.Model.Authorization.Permissions;

/// <summary>
/// Default permissions for the platform.
/// 
/// These permissions are the default permissions given to a user when they sign up.
/// </summary>
public static class DefaultPermissions
{
    public static readonly Permission[] PlatformPermissions = [
        Permission.CragsRead,
        Permission.RoutesRead,
        Permission.PitchesRead,
        Permission.NotesRead,
        Permission.OrganizationsRead,
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
    [JsonStringEnumMemberName("documents:read")]
    DocumentsRead,
    [JsonStringEnumMemberName("documents:write")]
    DocumentsWrite,
    [JsonStringEnumMemberName("tags:read")]
    TagsRead,
    [JsonStringEnumMemberName("tags:write")]
    TagsWrite,
    [JsonStringEnumMemberName("crags:read")]
    CragsRead,
    [JsonStringEnumMemberName("crags:write")]
    CragsWrite,
    [JsonStringEnumMemberName("routes:read")]
    RoutesRead,
    [JsonStringEnumMemberName("routes:write")]
    RoutesWrite,
    [JsonStringEnumMemberName("pitches:read")]
    PitchesRead,
    [JsonStringEnumMemberName("pitches:write")]
    PitchesWrite,
    [JsonStringEnumMemberName("notes:read")]
    NotesRead,
    [JsonStringEnumMemberName("notes:write")]
    NotesWrite,
}