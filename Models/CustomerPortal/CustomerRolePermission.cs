public class CustomerRolePermission
{
    public long CustomerRoleId { get; set; }

    public long CustomerPermissionId { get; set; }

    public CustomerRole CustomerRole { get; set; } = null!;

    public CustomerPermission CustomerPermission { get; set; } = null!;
}
