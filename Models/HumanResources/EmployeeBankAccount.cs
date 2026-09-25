using System.ComponentModel.DataAnnotations;

public class EmployeeBankAccount
{
    public long Id { get; set; }

    public long EmployeeId { get; set; }

    [MaxLength(100)]
    public string BankName { get; set; } = string.Empty;

    [MaxLength(30)]
    public string AccountNumber { get; set; } = string.Empty;

    [MaxLength(30)]
    public string? Cci { get; set; }

    public Employee? Employee { get; set; }
}
