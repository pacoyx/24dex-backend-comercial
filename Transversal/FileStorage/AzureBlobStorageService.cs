using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;

public class AzureBlobStorageService : IFileStorageService
{
    private readonly BlobContainerClient _containerClient;
    private bool _containerEnsured;

    public AzureBlobStorageService(IOptions<FileStorageSettings> settings)
    {
        var blobServiceClient = new BlobServiceClient(settings.Value.ConnectionString);
        _containerClient = blobServiceClient.GetBlobContainerClient(settings.Value.ContainerName);
    }

    public async Task<StorageFileResult> UploadAsync(
        string category,
        long pickupGuideId,
        string originalFileName,
        string contentType,
        Stream content,
        CancellationToken cancellationToken = default)
    {
        await EnsureContainerAsync(cancellationToken);

        var extension = Path.GetExtension(originalFileName);
        var storedFileName = $"pickup-guide-{pickupGuideId}-{Guid.NewGuid():N}{extension}";
        var blobPath = $"{pickupGuideId}/{category.ToLowerInvariant()}/{storedFileName}";

        var blobClient = _containerClient.GetBlobClient(blobPath);
        await blobClient.UploadAsync(content, new Azure.Storage.Blobs.Models.BlobHttpHeaders
        {
            ContentType = contentType
        }, cancellationToken: cancellationToken);

        var properties = await blobClient.GetPropertiesAsync(cancellationToken: cancellationToken);

        return new StorageFileResult(storedFileName, blobPath, properties.Value.ContentLength);
    }

    public async Task DeleteAsync(string filePath, CancellationToken cancellationToken = default)
    {
        await EnsureContainerAsync(cancellationToken);

        var blobClient = _containerClient.GetBlobClient(filePath);
        await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
    }

    public async Task<Stream> OpenReadAsync(string filePath, CancellationToken cancellationToken = default)
    {
        await EnsureContainerAsync(cancellationToken);

        var blobClient = _containerClient.GetBlobClient(filePath);
        var download = await blobClient.DownloadStreamingAsync(cancellationToken: cancellationToken);
        return download.Value.Content;
    }

    private async Task EnsureContainerAsync(CancellationToken cancellationToken)
    {
        if (_containerEnsured)
        {
            return;
        }

        await _containerClient.CreateIfNotExistsAsync(cancellationToken: cancellationToken);
        _containerEnsured = true;
    }
}
