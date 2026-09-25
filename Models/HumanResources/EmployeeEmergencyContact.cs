using System.ComponentModel.DataAnnotations;

public class EmployeeEmergencyContact
{
    public long Id { get; set; }

    public long EmployeeId { get; set; }

    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Relationship { get; set; } = string.Empty;

    [MaxLength(20)]
    public string PhoneNumber { get; set; } = string.Empty;

    public Employee? Employee { get; set; }
}
