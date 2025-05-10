using Microsoft.EntityFrameworkCore;
using Dotnet.Playground.Model.Media.Image;

namespace Dotnet.Playground.DI.Data.ConfigurationExtension;

public static class ImageConfigurationExtension
{
    public static ModelBuilder ConfigureImageHierarchyModel(this ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Image>()
        .HasOne(e => e.Uploader)
        .WithMany()
        .HasForeignKey(e => e.UploaderId);

      modelBuilder.Entity<AreaImage>()
        .HasOne(e => e.Area)
        .WithMany(e => e.Images)
        .HasForeignKey(e => e.AreaId);

      modelBuilder.Entity<SectorImage>()
        .HasOne(e => e.Sector)
        .WithMany(e => e.Images)
        .HasForeignKey(e => e.SectorId);

      modelBuilder.Entity<PitchImage>()
        .HasOne(e => e.Pitch)
        .WithOne(e => e.TopoImage)
        .HasForeignKey<PitchImage>(e => e.PitchId);

      modelBuilder.Entity<RouteImage>()
        .HasOne(e => e.Route)
        .WithOne(e => e.TopoImage)
        .HasForeignKey<RouteImage>(e => e.RouteId);

      return modelBuilder;
    }
} 