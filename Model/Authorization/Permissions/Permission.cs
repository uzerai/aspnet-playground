using System.Text.Json.Serialization;
using Uzerai.Dotnet.Playground.DI.Authorization.Permissions;

namespace Uzerai.Dotnet.Playground.Model.Authorization.Permissions;

[JsonConverter(typeof(JsonPermissionsConverter))]
public enum Permission
{
    UsersRead,
    UsersWrite,
    OrganizationsRead,
    OrganizationsWrite,
    PermissionsRead,
    PermissionsWrite,
    TeamsRead,
    TeamsWrite,
}