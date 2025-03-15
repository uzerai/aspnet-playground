using Microsoft.EntityFrameworkCore;
using Dotnet.Playground.Model;

namespace Dotnet.Playground.DI.Data.ConfigurationExtension;

public static class BaseEntityConfigurationExtension
{
    public static ModelBuilder ConfigureBaseEntityAbstractModel(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BaseEntity>()
          .UseTpcMappingStrategy();
        
        return modelBuilder;
    }
}
