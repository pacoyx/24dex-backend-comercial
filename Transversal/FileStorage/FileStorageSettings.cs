public class FileStorageSettings
{
    public bool Enabled { get; set; } = true;

    public string Provider { get; set; } = "Local";

    public string BasePath { get; set; } = "Uploads";

    public string ConnectionString { get; set; } = string.Empty;

    public string ContainerName { get; set; } = "pickup-guides";
}
