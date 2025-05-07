using Microsoft.EntityFrameworkCore;
using Dotnet.Playground.Model;
using Dotnet.Playground.Model.Organizations;

namespace Dotnet.Playground.DI.Data.ConfigurationExtension;

public static class CragConfigurationExtension
{
    public static ModelBuilder ConfigureCragModel(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Crag>()
            .HasOne(e => e.MaintainerOrganization)
            .WithMany()
            .HasForeignKey(e => e.MaintainerOrganizationId);

        modelBuilder.Entity<Crag>()
            .HasMany(e => e.Sectors)
            .WithOne(e => e.Crag);

        return modelBuilder;
    }
} 