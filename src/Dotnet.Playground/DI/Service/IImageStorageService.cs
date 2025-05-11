using Dotnet.Playground.Model;
using Dotnet.Playground.Model.Authentication;

namespace Dotnet.Playground.DI.Service;

// TODO: Split IImageStorageService into MinioService and ImageService to
// handle the minio-specific logic and the database image entity logic separately later.
public interface IImageStorageService
{
    Task<Image> UploadImage(Stream image, User user, Guid? relatedEntityId);
    Task<Image> RemoveImage(Guid imageId);
}
