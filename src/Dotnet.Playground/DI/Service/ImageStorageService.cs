using Dotnet.Playground.Model;
using Dotnet.Playground.Model.Authentication;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;

namespace Dotnet.Playground.DI.Service;

public class ImageStorageService : IImageStorageService
{
    private static readonly string BUCKET_NAME = "images";
    private readonly IMinioClient _minioClient;
    private readonly ILogger<ImageStorageService> _logger;

    public ImageStorageService(IMinioClient minioClient, ILogger<ImageStorageService> logger)
    {
        _minioClient = minioClient;
        _logger = logger;
    }

    public async Task<Image> UploadImage(Stream image, User user, Guid relatedEntityId)
    {
        try
        {
            _logger.LogInformation("Uploading image to bucket {bucketName}", BUCKET_NAME);

            bool bucketExists = await _minioClient.BucketExistsAsync(
                new BucketExistsArgs().WithBucket(BUCKET_NAME)).ConfigureAwait(false);

            _logger.LogInformation("Bucket exists: {bucketExists}", bucketExists);

            if (!bucketExists)
            {
                _logger.LogInformation("Bucket {bucketName} does not exist, creating it.", BUCKET_NAME);
                await _minioClient.MakeBucketAsync(new MakeBucketArgs()
                    .WithBucket(BUCKET_NAME)).ConfigureAwait(false);
            }

            var objectName = Guid.NewGuid().ToString();
            var args = new PutObjectArgs()
                .WithBucket(BUCKET_NAME)
                .WithObject(objectName)
                .WithContentType("application/octet-stream")
                .WithStreamData(image)
                .WithObjectSize(image.Length);

            await _minioClient.PutObjectAsync(args).ConfigureAwait(false);

            return new Image
            {
                Key = objectName,
                Bucket = BUCKET_NAME,
                Url = new Uri($"https://minio.playground.com/{BUCKET_NAME}/{objectName}"),
                UploaderId = user.Id,
                RelatedEntityId = relatedEntityId,
            };
        }
        catch (MinioException ex)
        {
            _logger.LogError(ex, "Error occurred while uploading image to MinIO");
            throw;
        }
    }
}