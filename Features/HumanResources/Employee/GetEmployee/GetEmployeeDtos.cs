public record EmergencyContactDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Relationship { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}

public record BankAccountDto
{
    public long Id { get; set; }
    public string BankName { get; set; } = string.Empty;
    public string AccountNumber { get; set; } = string.Empty;
    public string? Cci { get; set; }
}

public record EmployeeDetailResponseDto
{
    public long Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string DocumentNumber { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string? Address { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime HireDate { get; set; }
    public DateTime? TerminationDate { get; set; }
    public bool IsActive { get; set; }
    public int AreaId { get; set; }
    public string AreaName { get; set; } = string.Empty;
    public int PositionId { get; set; }
    public string PositionName { get; set; } = string.Empty;
    public EmployeeContractType ContractType { get; set; }
    public decimal BaseSalary { get; set; }
    public List<EmergencyContactDto> EmergencyContacts { get; set; } = new();
    public List<BankAccountDto> BankAccounts { get; set; } = new();
}
