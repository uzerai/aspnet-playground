using Microsoft.EntityFrameworkCore;
using Uzerai.Dotnet.Playground.Model.Authentication;
using Uzerai.Dotnet.Playground.Model.Organizations;

namespace Uzerai.Dotnet.Playground.DI.Data.ConfigurationExtension;

public static class UserConfigurationExtension
{
    public static ModelBuilder ConfigureUserModel(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(e => e.Organizations)
            .WithMany(e => e.Users)
            .UsingEntity<OrganizationUser>();
        modelBuilder.Entity<User>()
            .HasMany(e => e.OrganizationUsers)
            .WithOne(e => e.User);
            
        return modelBuilder;
    } 
}