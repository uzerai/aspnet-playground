using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NodaTime;
using Uzerai.Dotnet.Playground.DI.Data;
using Uzerai.Dotnet.Playground.DI.Repository.ConfigurationExtension;

namespace Dotnet.Playground.IntegrationTests;

public class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // Call base first
        base.ConfigureWebHost(builder);
        builder.UseEnvironment("Test");
        
        builder.ConfigureServices(services =>
        {
            // Configure services similar to your main application
            services.AddControllers();
            services.AddRouting();

            services.Configure<DbContextOptionsBuilder>(options => {
                options.UseNpgsql("Host=localhost;Database=playground_test;Username=playground;Password=playground;Port=5432");
            });
            
            services.RemoveAll<AuthenticationSchemeOptions>();
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = TestAuthenticationHandler.AuthenticationScheme;
                options.DefaultChallengeScheme = TestAuthenticationHandler.AuthenticationScheme;
            }).AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>(
                TestAuthenticationHandler.AuthenticationScheme,
                options => {});

            RegisterServices(services);
            
            // Build the service provider to initialize the database
            var sp = services.BuildServiceProvider();
            
            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<DatabaseContext>();
                
                // Ensure the database is created
                db.Database.EnsureCreated();
            }
        });
    }

    protected void RegisterServices(IServiceCollection services) {
        //TODO: Make this available to configure/override as part of the test suite settings.
        services.AddSingleton<IClock>(NodaTime.SystemClock.Instance);
        services.AddRepositories();
    }
}
