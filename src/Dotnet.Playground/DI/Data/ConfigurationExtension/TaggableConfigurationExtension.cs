using Microsoft.EntityFrameworkCore;
using Uzerai.Dotnet.Playground.Model.Tags;

namespace Uzerai.Dotnet.Playground.DI.Data.ConfigurationExtension;

public static class TaggableConfigurationExtension
{
    public static ModelBuilder ConfigureTaggable(this ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Taggable>().UseTpcMappingStrategy();

      return modelBuilder;
    }
}