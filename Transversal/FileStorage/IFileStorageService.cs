public interface IFileStorageService
{
    Task<StorageFileResult> UploadAsync(
        string topFolder,
        string category,
        long entityId,
        string originalFileName,
        string contentType,
        Stream content,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(string filePath, CancellationToken cancellationToken = default);

    Task<Stream> OpenReadAsync(string filePath, CancellationToken cancellationToken = default);
}
