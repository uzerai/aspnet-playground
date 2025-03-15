using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Dotnet.Playground.Model.Authorization.Permissions;

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
}