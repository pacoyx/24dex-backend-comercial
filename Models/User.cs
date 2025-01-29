using System.ComponentModel.DataAnnotations;

public class User
{
    public int Id { get; set; }
    [MaxLength(200)]
    public required string Name { get; set; }
    [MaxLength(50)]
    public required string UserName { get; set; }
    public required string Password { get; set; }
    [MaxLength(20)]
    public required string Role { get; set; }
    [MaxLength(1)]
    public required string Status { get; set; }        
    public List<BrachSalesUser> BrachSalesUsers { get; set; } = [];
    public string RefreshToken { get; set; } = "";
    public string HashPassword { get; set; } = "";
    [MaxLength(200)]
    public string Email { get; set; } = "";
}