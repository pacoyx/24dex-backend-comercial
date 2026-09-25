using System.ComponentModel.DataAnnotations;

public class Employee
{
    public long Id { get; set; }

    [MaxLength(20)]
    public string Code { get; set; } = string.Empty;

    [MaxLength(20)]
    public string DocumentNumber { get; set; } = string.Empty;

    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    [MaxLength(20)]
    public string PhoneNumber { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Address { get; set; }

    public DateTime BirthDate { get; set; }

    public DateTime HireDate { get; set; }

    public DateTime? TerminationDate { get; set; }

    public bool IsActive { get; set; } = true;

    public int AreaId { get; set; }

    public int PositionId { get; set; }

    public EmployeeContractType ContractType { get; set; }

    public decimal BaseSalary { get; set; }

    public Area Area { get; set; } = null!;

    public Position Position { get; set; } = null!;

    public List<EmployeeEmergencyContact>? EmergencyContacts { get; set; }

    public List<EmployeeBankAccount>? BankAccounts { get; set; }
}
