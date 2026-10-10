public interface IFileStorageService
{
    Task<StorageFileResult> UploadAsync(
        string category,
        long pickupGuideId,
        string originalFileName,
        string contentType,
        Stream content,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(string filePath, CancellationToken cancellationToken = default);

    Task<Stream> OpenReadAsync(string filePath, CancellationToken cancellationToken = default);
}
