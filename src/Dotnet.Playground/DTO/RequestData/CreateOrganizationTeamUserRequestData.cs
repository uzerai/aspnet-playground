using Dotnet.Playground.Model.Authorization.Permissions;

namespace Dotnet.Playground.DTO.RequestData;

public class CreateOrganizationTeamUserRequestData
{
    public required Guid UserId { get; set; }
    public required Guid OrganizationTeamId { get; set; }
    public required List<Permission> Permissions { get; set; }
}
