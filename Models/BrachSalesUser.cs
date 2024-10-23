public class BrachSalesUser{    
    public int Id { get; set; }
    public int BranchSalesId { get; set; }
    public BranchSales? BranchSales { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }   
    public string Status { get; set; } = string.Empty;

}