using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Uzerai.Dotnet.Playground.Controllers.CreateModel;
using Uzerai.Dotnet.Playground.Model.Organizations;

namespace Dotnet.Playground.IntegrationTests.Controllers;

public class OrganizationsControllerIntegrationTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;
    
    public OrganizationsControllerIntegrationTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
        
        // We would normally setup authentication here
        // This is a placeholder for actual authentication setup
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Im5hZzAiLCJzdWIiOiJuYWcwIiwianRpIjoiOGE3NGFkMjYiLCJhdWQiOlsiaHR0cDovL2xvY2FsaG9zdDo0NDA0NiIsImh0dHBzOi8vbG9jYWxob3N0OjQ0MzMyIiwiaHR0cDovL2xvY2FsaG9zdDo1MDE2IiwiaHR0cHM6Ly9sb2NhbGhvc3Q6NzAzNCJdLCJuYmYiOjE3NDIwMzcxMDIsImV4cCI6MTc0OTk4NTkwMiwiaWF0IjoxNzQyMDM3MTAyLCJpc3MiOiJkb3RuZXQtdXNlci1qd3RzIn0.0fOAZZMCqGG8mJcTg8FOseYCF47Xbrj6OrmEbrpyy1E");
    }
    
    [Fact]
    public async Task GetAll_ReturnsSuccessStatusCode()
    {
        // Act
        var response = await _client.GetAsync("/organizations");
                
        // Assert
        response.EnsureSuccessStatusCode();
    }
    
    [Fact]
    public async Task Create_WithValidData_ReturnsCreatedOrganization()
    {
        // Arrange
        var createRequest = new CreateOrganizationRequestData("Integration Test Org");
        
        var content = new StringContent(
            JsonSerializer.Serialize(createRequest),
            Encoding.UTF8,
            "application/json");
            
        // Act
        var response = await _client.PostAsync("/organizations", content);
        
        // Assert
        response.EnsureSuccessStatusCode();
        
        var responseString = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        
        var organization = JsonSerializer.Deserialize<Organization>(responseString, options);
        Assert.NotNull(organization);
        Assert.Equal("Integration Test Org", organization.Name);
    }
}
