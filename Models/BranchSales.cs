public class BranchSales
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public List<BrachSalesUser> BrachSalesUsers { get; set; } = [];
}