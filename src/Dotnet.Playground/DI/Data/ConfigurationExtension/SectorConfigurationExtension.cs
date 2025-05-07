using Microsoft.EntityFrameworkCore;
using Dotnet.Playground.Model;

namespace Dotnet.Playground.DI.Data.ConfigurationExtension;

public static class SectorConfigurationExtension
{
    public static ModelBuilder ConfigureSectorModel(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Sector>()
            .HasOne(e => e.Crag)
            .WithMany(e => e.Sectors)
            .HasForeignKey(e => e.CragId);
        
        modelBuilder.Entity<Sector>()
            .HasMany(e => e.Routes)
            .WithOne(e => e.Sector);

        return modelBuilder;
    }
} 