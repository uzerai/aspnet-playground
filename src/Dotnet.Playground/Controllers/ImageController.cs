using Dotnet.Playground.DI;
using Dotnet.Playground.DI.Service;
using Dotnet.Playground.Model;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Playground.Controllers;

[ApiController]
[Route("images")]
public class ImageController : ControllerBase
{ 
    private readonly IImageStorageService _imageStorageService;

    public ImageController(IImageStorageService imageStorageService)
    {
        _imageStorageService = imageStorageService;
    }
    
    [HttpPost]
    public async Task<ActionResult<Image>> UploadImage()
    {
        using var memoryStream = new MemoryStream();
        await Request.Body.CopyToAsync(memoryStream);
        memoryStream.Position = 0;

        var uploadedImage = await _imageStorageService.UploadImage(memoryStream, HttpContext.GetLocalUser(), Guid.Empty);

        return Created(uploadedImage.Id.ToString(), uploadedImage);
    }

}
