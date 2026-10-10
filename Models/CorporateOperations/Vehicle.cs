using System.ComponentModel.DataAnnotations;

public class Vehicle
{
    public int Id { get; set; }

    [MaxLength(20)]
    public string Plate { get; set; } = string.Empty;

    [MaxLength(200)]
    public string Description { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
}
