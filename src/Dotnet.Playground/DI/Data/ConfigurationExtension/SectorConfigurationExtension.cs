using Microsoft.EntityFrameworkCore;
using Dotnet.Playground.Model;

namespace Dotnet.Playground.DI.Data.ConfigurationExtension;

public static class SectorConfigurationExtension
{
    public static ModelBuilder ConfigureSectorModel(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Sector>()
            .HasOne(e => e.Area)
            .WithMany(e => e.Sectors)
            .HasForeignKey(e => e.AreaId);
        
        modelBuilder.Entity<Sector>()
            .HasMany(e => e.Routes)
            .WithOne(e => e.Sector);

        modelBuilder.Entity<Sector>()
            .HasMany(e => e.Images)
            .WithOne()
            .HasForeignKey(e => e.SectorId);

        return modelBuilder;
    }
} 