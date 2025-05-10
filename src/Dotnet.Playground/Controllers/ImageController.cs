using Dotnet.Playground.DI;
using Dotnet.Playground.DI.Service;
using Dotnet.Playground.Model;
using Microsoft.AspNetCore.Mvc;
using Dotnet.Playground.DI.Authorization.UserContext;
using Dotnet.Playground.Model.Authorization.Permissions;
using Dotnet.Playground.DI.Authorization.Permissions;

namespace Dotnet.Playground.Controllers;

[ApiController]
[Route("images")]
public class ImageController : ControllerBase
{ 
    private readonly IImageStorageService _imageStorageService;
    private readonly IUserContext _userContext;

    public ImageController(
        IImageStorageService imageStorageService,
        IUserContext userContext)
    {
        _imageStorageService = imageStorageService;
        _userContext = userContext;
    }
    
    [PlatformPermissionRequired(Permission.ImagesWrite)]
    [HttpPost]
    public async Task<ActionResult<Image>> UploadImage()
    {
        using var memoryStream = new MemoryStream();
        await Request.Body.CopyToAsync(memoryStream);
        memoryStream.Position = 0;

        var uploadedImage = await _imageStorageService.UploadImage(
            memoryStream, 
            _userContext.CurrentUser!, 
            Guid.Empty);

        return Created(uploadedImage.Id.ToString(), uploadedImage);
    }
}
