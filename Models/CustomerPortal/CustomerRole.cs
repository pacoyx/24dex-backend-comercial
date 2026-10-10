public class CustomerRole
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public ICollection<CustomerRolePermission> Permissions { get; set; }
        = new List<CustomerRolePermission>();
}
