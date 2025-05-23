
using Dotnet.Playground.Model.Location;
using Microsoft.EntityFrameworkCore;
using Route = Dotnet.Playground.Model.Location.Route;

namespace Dotnet.Playground.DI.Data.ConfigurationExtension;

public static class RouteConfigurationExtension
{
    public static ModelBuilder ConfigureRouteModel(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Route>()
            .HasOne(e => e.Sector)
            .WithMany(e => e.Routes)
            .HasForeignKey(e => e.SectorId);

        modelBuilder.Entity<Route>()
            .HasMany(e => e.Pitches)
            .WithMany(e => e.Routes)
            .UsingEntity<RoutePitch>();

        modelBuilder.Entity<Route>()
            .HasOne(e => e.TopoImage)
            .WithOne(e => e.RelatedEntity as Route)
            .HasForeignKey<Route>(e => e.TopoImageId);

        return modelBuilder;
    }
} 