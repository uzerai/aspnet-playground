using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NodaTime;
using Uzerai.Dotnet.Playground.DI.Data;

namespace Dotnet.Playground.IntegrationTests;

public class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override IHostBuilder CreateHostBuilder()
    {
        // Create a host builder manually since we can't access Program.CreateHostBuilder
        return Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Program>();
                // Configure the web host
                webBuilder.UseTestServer();
            });
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // Call base first
        base.ConfigureWebHost(builder);

        builder.ConfigureServices(services =>
        {
            // Configure services similar to your main application
            services.AddControllers();
            services.AddRouting();
            
            // Add other services your application needs
            RegisterServices(services);
            
            // Remove any DbContext registrations
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<DatabaseContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }
            
            // Add in-memory database
            services.AddDbContext<DatabaseContext>((options, context) =>
            {
                context.UseInMemoryDatabase("InMemoryDbForTesting");
            });
            
            // Build the service provider to initialize the database
            var sp = services.BuildServiceProvider();
            
            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<DatabaseContext>();
                
                // Ensure the database is created
                db.Database.EnsureCreated();
                
                // Seed test data here if needed
            }
        });
    }

    protected void RegisterServices(IServiceCollection services) {
        services.AddSingleton<IClock>(SystemClock.Instance);
    }
}
