using Microsoft.EntityFrameworkCore;
using Uzerai.Dotnet.Playground.Model.Organizations;

namespace Uzerai.Dotnet.Playground.DI.Data.ConfigurationExtension;

public static class OrganizationConfigurationExtension
{
    public static ModelBuilder ConfigureOrganizationModel(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Organization>()
            .HasMany(e => e.OrganizationUsers)
            .WithOne(e => e.Organization);
        modelBuilder.Entity<Organization>()
            .HasMany(e => e.Users)
            .WithMany(e => e.Organizations)
            .UsingEntity<OrganizationUser>();
        
        return modelBuilder;
    }
}