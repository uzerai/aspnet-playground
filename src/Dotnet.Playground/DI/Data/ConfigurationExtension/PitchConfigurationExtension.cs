using Microsoft.EntityFrameworkCore;
using Dotnet.Playground.Model.Location;

namespace Dotnet.Playground.DI.Data.ConfigurationExtension;

public static class PitchConfigurationExtension
{
    public static ModelBuilder ConfigurePitchModel(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pitch>()
            .HasOne(e => e.Sector)
            .WithMany()
            .HasForeignKey(e => e.SectorId);

        modelBuilder.Entity<Pitch>()
            .HasMany(e => e.Routes)
            .WithMany(e => e.Pitches)
            .UsingEntity<RoutePitch>();
            
        return modelBuilder;
    }
} 