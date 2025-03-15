using Microsoft.EntityFrameworkCore;
using Dotnet.Playground.Model;
using Dotnet.Playground.Model.Tags;

namespace Dotnet.Playground.DI.Data.ConfigurationExtension;

public static class DocumentConfigurationExtension
{
    public static ModelBuilder ConfigureDocumentModel(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Document>()
            .HasOne(e => e.Organization)
            .WithMany()
            .HasForeignKey(e => e.OrganizationId);

        modelBuilder.Entity<Document>()
            .HasOne(e => e.Author)
            .WithMany()
            .HasForeignKey(e => e.AuthorId);
            
        modelBuilder.Entity<Document>()
            .HasMany(e => e.Tags)
            .WithMany(e => e.TaggedEntities as ICollection<Document>);
            
        return modelBuilder;
    }
}
