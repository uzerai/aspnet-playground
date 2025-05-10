using Dotnet.Playground.Model;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Playground.DI.Data.ConfigurationExtension;

public static class ImageConfigurationExtension
{
    public static ModelBuilder ConfigureImageModel(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Image>()
            .HasOne(e => e.Uploader)
            .WithMany()
            .HasForeignKey(e => e.UploaderId);

        return modelBuilder;
    }
}