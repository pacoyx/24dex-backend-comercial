
using System.ComponentModel.DataAnnotations;

public class Customer
{
    public int Id { get; set; }    
    [MaxLength(100)]
    public string FirtsName { get; set; } = string.Empty;
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;
    [MaxLength(200)]
    public string Address { get; set; } = string.Empty;
    [MaxLength(20)]
    public string Phone { get; set; } = string.Empty;
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;    
    [MaxLength(20)]
    public string DocPersonal { get; set; } = string.Empty;
    [MaxLength(1)]
    public string Status { get; set; } = "A";
}