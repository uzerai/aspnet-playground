using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Uzerai.Dotnet.Playground.Controllers.CreateModel;
using Xunit;

namespace Dotnet.Playground.Tests.IntegrationTests.Controllers;

public class OrganizationsControllerIntegrationTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;
    
    public OrganizationsControllerIntegrationTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
        
        // We would normally setup authentication here
        // This is a placeholder for actual authentication setup
        _client.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", "test_token");
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
        
        var organization = JsonSerializer.Deserialize<dynamic>(responseString, options);
        Assert.NotNull(organization);
        Assert.Equal("Integration Test Org", organization.GetProperty("name").GetString());
    }
}
