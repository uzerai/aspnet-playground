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

        return modelBuilder;
    }
} 