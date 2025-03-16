using Microsoft.EntityFrameworkCore;
using Dotnet.Playground.Model;

namespace Dotnet.Playground.DI.Data.ConfigurationExtension;

public static class NoteConfigurationExtension
{
    public static ModelBuilder ConfigureNoteModel(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Note>()
            .HasOne(e => e.Author)
            .WithMany()
            .HasForeignKey(e => e.AuthorId);

        modelBuilder.Entity<Note>()
            .HasOne(e => e.Crag)
            .WithMany(e => e.Notes)
            .HasForeignKey(e => e.CragId);

        return modelBuilder;
    }
} 