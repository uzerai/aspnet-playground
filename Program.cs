using Microsoft.EntityFrameworkCore;
using NodaTime;
using Npgsql;
using Uzerai.Dotnet.Playground.DI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using NodaTime.Serialization.SystemTextJson;
using Uzerai.Dotnet.Playground.API.DI.Middleware;
using Uzerai.Dotnet.Playground.DI.Repository;

// ############################################################
// ##########  APP BUILDING  ##################################
// ############################################################
var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddJsonOptions(options => {
    options.JsonSerializerOptions.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
});

// This technically isn't active currently.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}";
        options.Audience = builder.Configuration["Auth0:Audience"];

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            
            NameClaimType = ClaimTypes.NameIdentifier,
            ValidIssuer = builder.Configuration["Auth0:Issuer"],
            ValidAudience = builder.Configuration["Auth0:Audience"],
        };
    });

// Add NodaTime clock service so we can use it in the database context for timestamping BaseEntity objects.
// This is the SystemClock for the running version of the server. 
// Replace it in the test environment to be whatever you wish.
builder.Services.AddSingleton<IClock>(SystemClock.Instance);

// Add main application database context.
// Will contain references to all entities in the application.
builder.Services.AddDbContext<DatabaseContext>(options => {
        options.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseContextConnection"), npgsqlSourceBuilder => {
            // Additional configuration for the Npgsql connection.

            // Here used to enable NodaTime support.
            npgsqlSourceBuilder.UseNodaTime();
            npgsqlSourceBuilder.MigrationsHistoryTable("migrations");
        })
        .UseSnakeCaseNamingConvention();
    }
);

builder.Services.AddScoped<UserRepository>();

// ############################################################
// ##########  APP INITIALIZATION  ############################
// ############################################################
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
} else {
    app.UseHttpsRedirection();
}

// HTTPs redirection by default.
// App library & overarching middleware registration.
app.UseRouting();

// Authentication middleware.
app.UseAuthentication();

// Authorize local user middleware, this is what provides the link from an externally authorized user
// to the authenticated identity of the user in our system.
app.UseMiddleware<LocalUserContextMiddleware>();

// Authorization middleware.
app.UseAuthorization();
app.MapControllers();

app.Run();
