using Microsoft.EntityFrameworkCore;
using Dotnet.Playground.Model.Tags;

namespace Dotnet.Playground.DI.Data.ConfigurationExtension;

public static class TaggableConfigurationExtension
{
    public static ModelBuilder ConfigureTaggable(this ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Taggable>().UseTpcMappingStrategy();

      return modelBuilder;
    }
}