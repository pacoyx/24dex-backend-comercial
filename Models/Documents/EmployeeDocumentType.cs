using System.ComponentModel.DataAnnotations;

public class EmployeeDocumentType
{
    public int Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
}
