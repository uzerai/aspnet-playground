using Microsoft.EntityFrameworkCore;
using Dotnet.Playground.Model;

namespace Dotnet.Playground.DI.Data.ConfigurationExtension;

public static class PitchConfigurationExtension
{
    public static ModelBuilder ConfigurePitchModel(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pitch>()
            .HasOne(e => e.Crag)
            .WithMany()
            .HasForeignKey(e => e.CragId);

        modelBuilder.Entity<Pitch>()
            .HasMany(e => e.Routes)
            .WithMany(e => e.Pitches);

        return modelBuilder;
    }
} 