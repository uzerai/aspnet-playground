using Microsoft.EntityFrameworkCore;
using Uzerai.Dotnet.Playground.Model.Authorization.Permissions;

namespace Uzerai.Dotnet.Playground.DI.Data.ConfigurationExtension;

public static class OrganizationPermissionConfigurationExtension
{
    public static ModelBuilder ConfigureOrganizationPermissionModel(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrganizationPermission>()
            .HasKey(e => new { e.OrganizationId, e.UserId, e.Permission });
        modelBuilder.Entity<OrganizationPermission>()
            .HasOne(e => e.OrganizationUser)
            .WithMany(e => e.Permissions)
            .HasForeignKey(e => new { e.OrganizationId, e.UserId });

        return modelBuilder;
    }
}