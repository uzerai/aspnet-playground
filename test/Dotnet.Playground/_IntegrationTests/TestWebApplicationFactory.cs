using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Uzerai.Dotnet.Playground.DI.Data;

namespace Dotnet.Playground.Test.IntegrationTests
{
    // Create a marker class that we'll use instead of Program
    public class TestStartup 
    {
        // This can be empty
    }

    public class TestWebApplicationFactory : WebApplicationFactory<TestStartup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // Use your application's startup by replacing the configure callback
            builder.UseStartup<TestStartup>();
            
            builder.ConfigureServices(services =>
            {
                // Remove the app's DatabaseContext registration
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<DatabaseContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }
                
                // Add DatabaseContext using an in-memory database for testing
                services.AddDbContext<DatabaseContext>((options, context) =>
                {
                    context.UseInMemoryDatabase("InMemoryDbForTesting");
                });
                
                // Build the service provider
                var sp = services.BuildServiceProvider();
                
                // Create a scope to obtain a reference to the database context
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<DatabaseContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<TestWebApplicationFactory>>();
                    
                    // Ensure the database is created
                    db.Database.EnsureCreated();
                    
                    try
                    {
                        // Seed the database with test data
                        // This would call your TestDataSeeder if you had one
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the database. Error: {Message}", ex.Message);
                    }
                }
            });
        }
    }
}