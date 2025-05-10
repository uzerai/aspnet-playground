using Dotnet.Playground.Model;
using Dotnet.Playground.Model.Authentication;

namespace Dotnet.Playground.DI.Service;

public interface IImageStorageService
{
    Task<Image> UploadImage(Stream image, User user, Guid relatedEntityId);
}
