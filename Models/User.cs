public class User
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public required string Role { get; set; }
    public required string Status { get; set; }        
    public List<BrachSalesUser> BrachSalesUsers { get; set; } = [];
}