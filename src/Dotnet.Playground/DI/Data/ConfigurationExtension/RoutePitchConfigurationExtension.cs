using Microsoft.EntityFrameworkCore;
using Dotnet.Playground.Model.Organizations;
using Dotnet.Playground.Model;

namespace Dotnet.Playground.DI.Data.ConfigurationExtension;

public static class RoutePitchConfigurationExtension
{
    public static ModelBuilder ConfigureRoutePitchModel(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RoutePitch>()
            .HasKey(e => new { e.RouteId, e.PitchId });
        modelBuilder.Entity<RoutePitch>()
            .HasOne(e => e.Route)
            .WithMany()
            .HasForeignKey(e => e.RouteId);
        modelBuilder.Entity<RoutePitch>()
            .HasOne(e => e.Pitch)
            .WithMany()
            .HasForeignKey(e => e.PitchId);

        return modelBuilder;
    }

}