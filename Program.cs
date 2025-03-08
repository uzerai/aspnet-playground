using Microsoft.EntityFrameworkCore;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Options;


// ############################################################
// ##########  APP BUILDING  ##################################
// ############################################################
var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

// This technically isn't active currently.
// builder.Services.AddAuthentication(options => {
//     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
// }).AddJwtBearer(options => {
//     options.Authority = builder.Configuration["Auth0:Authority"];
//     options.Audience = builder.Configuration["Auth0:Audience"];
// });

//TODO: Remove this once we have an external implicit flow implementation.
// 
builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = builder.Configuration["Auth0:Domain"]!;
    options.ClientId = builder.Configuration["Auth0:ClientId"]!;
    options.ClientSecret = builder.Configuration["Auth0:ClientSecret"]!;
}).WithAccessToken(options =>
{
    options.Audience = builder.Configuration["Auth0:Audience"]!;
});

// Add main application database context.
// Will contain references to all entities in the application.
builder.Services.AddDbContextPool<DatabaseContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseContextConnection"))
);

// ############################################################
// ##########  APP INITIALIZATION  ############################
// ############################################################
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTPs redirection by default.
app.UseHttpsRedirection();
// App library & overarching middleware registration.
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
