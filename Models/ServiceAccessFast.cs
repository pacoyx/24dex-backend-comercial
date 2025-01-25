using System.ComponentModel.DataAnnotations;

public class ServiceAccessFast
{
    public int Id { get; set; } 
    public int ProdServiceID { get; set; }
    [MaxLength(100)]
    public string ShortName { get; set; } = "";
    [MaxLength(50)]
    public string IconName { get; set; } = "bolt";
    [MaxLength(1)]
    public string Status { get; set; } = "A";
    public int Order { get; set; } 
}