using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Uzerai.Dotnet.Playground.Controllers;
using Uzerai.Dotnet.Playground.Controllers.CreateModel;
using Uzerai.Dotnet.Playground.DI.Repository.Interface;
using Uzerai.Dotnet.Playground.Model.Authentication;
using Uzerai.Dotnet.Playground.Model.Organizations;
using Xunit;

namespace Dotnet.Playground.Tests.Controllers;

public class OrganizationsControllerTests
{
    private readonly Mock<IOrganizationRepository> _mockOrganizationRepository;
    private readonly Mock<IOrganizationUserRepository> _mockOrganizationUserRepository;
    private readonly OrganizationsController _controller;
    private readonly Guid _userId = Guid.NewGuid();

    public OrganizationsControllerTests()
    {
        // Setup mocks
        _mockOrganizationRepository = new Mock<IOrganizationRepository>();
        _mockOrganizationUserRepository = new Mock<IOrganizationUserRepository>();
        
        // Create controller with mocked dependencies
        _controller = new OrganizationsController(
            _mockOrganizationRepository.Object,
            _mockOrganizationUserRepository.Object);
        
        // Setup HttpContext with mock user
        var user = new User { Id = _userId, Auth0UserId = "auth0|1234567890", Email = "test@example.com", Username = "test-username" };
        var httpContext = new DefaultHttpContext();
        httpContext.Items["LocalUserIdentity"] = user;
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };
    }

    [Fact]
    public async Task GetAll_ReturnsOkResultWithOrganizations()
    {
        // Arrange
        var organizations = new List<Organization>
        {
            new Organization { Id = Guid.NewGuid(), Name = "Org 1" },
            new Organization { Id = Guid.NewGuid(), Name = "Org 2" }
        };
        
        _mockOrganizationRepository.Setup(repo => 
            repo.GetAllAsync()).ReturnsAsync(organizations);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<Organization>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }

    [Fact]
    public async Task Get_WithValidId_ReturnsOkResultWithOrganization()
    {
        // Arrange
        var organizationId = Guid.NewGuid();
        var organization = new Organization { Id = organizationId, Name = "Test Org" };
        
        _mockOrganizationRepository.Setup(repo => 
            repo.GetByIdAsync(organizationId)).ReturnsAsync(organization);

        // Act
        var result = await _controller.Get(organizationId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<Organization>(okResult.Value);
        Assert.Equal(organizationId, returnValue.Id);
    }

    [Fact]
    public async Task Create_ValidOrganization_ReturnsCreatedOrganization()
    {
        // Arrange
        var createRequest = new CreateOrganizationRequestData("New Organization");
        
        var newOrganization = new Organization
        {
            Id = Guid.NewGuid(),
            Name = createRequest.Name
        };
        
        _mockOrganizationRepository.Setup(repo => 
            repo.CreateAsync(It.IsAny<Organization>()))
            .ReturnsAsync(newOrganization);
        
        _mockOrganizationUserRepository.Setup(repo => 
            repo.CreateAsync(It.IsAny<OrganizationUser>()))
            .ReturnsAsync(new OrganizationUser(){
                OrganizationId = newOrganization.Id,
                UserId = _userId
            });

        // Act
        var result = await _controller.Create(createRequest);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<Organization>(okResult.Value);
        Assert.Equal(newOrganization.Id, returnValue.Id);
        Assert.Equal(createRequest.Name, returnValue.Name);
        
        _mockOrganizationUserRepository.Verify(
            repo => repo.CreateAsync(It.Is<OrganizationUser>(ou => 
                ou.OrganizationId == newOrganization.Id && 
                ou.UserId == _userId)), 
            Times.Once);
    }
}