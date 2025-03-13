using Microsoft.EntityFrameworkCore;
using Uzerai.Dotnet.Playground.Model;

namespace Uzerai.Dotnet.Playground.DI.Data.ConfigurationExtension;

public static class BaseEntityConfigurationExtension
{
    public static ModelBuilder ConfigureBaseEntityAbstractModel(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BaseEntity>()
          .UseTpcMappingStrategy();
        
        return modelBuilder;
    }
}
