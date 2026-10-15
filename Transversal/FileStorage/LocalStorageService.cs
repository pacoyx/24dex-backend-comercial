using Microsoft.Extensions.Options;

public class LocalStorageService : IFileStorageService
{
    private readonly FileStorageSettings _settings;

    public LocalStorageService(IOptions<FileStorageSettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task<StorageFileResult> UploadAsync(
        string topFolder,
        string category,
        long entityId,
        string originalFileName,
        string contentType,
        Stream content,
        CancellationToken cancellationToken = default)
    {
        var extension = Path.GetExtension(originalFileName);
        var storedFileName = $"{topFolder.ToLowerInvariant()}-{entityId}-{Guid.NewGuid():N}{extension}";

        var directory = Path.Combine(_settings.BasePath, topFolder, entityId.ToString(), category);
        Directory.CreateDirectory(directory);

        var relativePath = Path.Combine(topFolder, entityId.ToString(), category, storedFileName)
            .Replace('\\', '/');
        var absolutePath = Path.Combine(directory, storedFileName);

        await using (var fileStream = new FileStream(absolutePath, FileMode.CreateNew, FileAccess.Write))
        {
            await content.CopyToAsync(fileStream, cancellationToken);
        }

        var fileSize = new FileInfo(absolutePath).Length;

        return new StorageFileResult(storedFileName, relativePath, fileSize);
    }

    public Task DeleteAsync(string filePath, CancellationToken cancellationToken = default)
    {
        var absolutePath = Path.Combine(_settings.BasePath, filePath);
        if (File.Exists(absolutePath))
        {
            File.Delete(absolutePath);
        }

        return Task.CompletedTask;
    }

    public Task<Stream> OpenReadAsync(string filePath, CancellationToken cancellationToken = default)
    {
        var absolutePath = Path.Combine(_settings.BasePath, filePath);
        Stream stream = new FileStream(absolutePath, FileMode.Open, FileAccess.Read);
        return Task.FromResult(stream);
    }
}
