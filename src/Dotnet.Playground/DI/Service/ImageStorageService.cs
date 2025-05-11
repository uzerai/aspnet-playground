using Dotnet.Playground.DI.Repository.Interface;
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
    private readonly IRepository<Image> _imageRepository;
    private readonly ILogger<ImageStorageService> _logger;

    public ImageStorageService(
        IMinioClient minioClient, 
        IRepository<Image> imageRepository, 
        ILogger<ImageStorageService> logger)
    {
        _minioClient = minioClient;
        _imageRepository = imageRepository;
        _logger = logger;
    }

    public async Task<Image> UploadImage(Stream image, User user, Guid? relatedEntityId)
    {
        try
        {
            _logger.LogInformation("Uploading image to bucket {bucketName}", BUCKET_NAME);

            bool bucketExists = await _minioClient.BucketExistsAsync(
                new BucketExistsArgs().WithBucket(BUCKET_NAME));

            _logger.LogInformation("Bucket exists: {bucketExists}", bucketExists);

            if (!bucketExists)
            {
                _logger.LogInformation("Bucket {bucketName} does not exist, creating it.", BUCKET_NAME);
                await _minioClient.MakeBucketAsync(new MakeBucketArgs()
                    .WithBucket(BUCKET_NAME));
            }

            var objectName = Guid.NewGuid().ToString();
            var args = new PutObjectArgs()
                .WithBucket(BUCKET_NAME)
                .WithObject(objectName)
                .WithContentType("image/jpeg")
                .WithStreamData(image)
                .WithObjectSize(image.Length);

            var uploadedObject = await _minioClient.PutObjectAsync(args);

            var url = await _minioClient.PresignedGetObjectAsync(
                new PresignedGetObjectArgs()
                    .WithBucket(BUCKET_NAME)
                    .WithObject(objectName)
                    .WithExpiry(60 * 60 * 24));

            Image imageEntity = await _imageRepository.CreateAsync(new()
            {
                Key = objectName,
                Bucket = BUCKET_NAME,
                UploaderId = user.Id,
                RelatedEntityId = relatedEntityId,
            });

            return imageEntity;
        }
        catch (MinioException ex)
        {
            _logger.LogError(ex, "Error occurred while uploading image to MinIO");
            throw;
        }
    }
}