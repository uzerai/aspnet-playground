using Microsoft.EntityFrameworkCore;
using Dotnet.Playground.Model;

namespace Dotnet.Playground.DI.Data.ConfigurationExtension;

public static class AreaConfigurationExtension
{
    public static ModelBuilder ConfigureAreaModel(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Area>()
            .HasOne(e => e.MaintainerOrganization)
            .WithMany()
            .HasForeignKey(e => e.MaintainerOrganizationId);

        modelBuilder.Entity<Area>()
            .HasMany(e => e.Sectors)
            .WithOne(e => e.Area);

        modelBuilder.Entity<Area>()
            .HasMany(e => e.Images)
            .WithOne(e => e.Area)
            .HasForeignKey(e => e.AreaId);

        return modelBuilder;
    }
} 