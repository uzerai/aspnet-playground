using Microsoft.EntityFrameworkCore;
using Dotnet.Playground.Model.Tags;

namespace Dotnet.Playground.DI.Data.ConfigurationExtension;

public static class TagConfigurationExtension
{
    public static ModelBuilder ConfigureTagModel(this ModelBuilder modelBuilder){
        
        modelBuilder.Entity<Tag>()
            .HasOne(e => e.Organization)
            .WithMany(e => e.Tags)
            .HasForeignKey(e => e.OrganizationId);

        modelBuilder.Entity<Tag>()
            .HasOne(e => e.CreatedBy)
            .WithMany()
            .HasForeignKey(e => e.CreatedById);

        modelBuilder.Entity<Tag>()
            .HasMany(e => e.TaggedEntities)
            .WithMany(e => e.Tags);

        return modelBuilder;
    }
}
