public abstract class BaseAuditableEntity
{
    public DateTime Created { get; set; }
    public string? CreatedBy { get; set; } = string.Empty;
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; } = string.Empty;
}