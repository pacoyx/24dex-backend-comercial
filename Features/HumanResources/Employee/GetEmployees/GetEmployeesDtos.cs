public record EmployeeListItemDto
{
    public long Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string DocumentNumber { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime HireDate { get; set; }
    public bool IsActive { get; set; }
    public int AreaId { get; set; }
    public string AreaName { get; set; } = string.Empty;
    public int PositionId { get; set; }
    public string PositionName { get; set; } = string.Empty;
    public EmployeeContractType ContractType { get; set; }
    public decimal BaseSalary { get; set; }
}

public record GetEmployeesResponseDto
{
    public List<EmployeeListItemDto> Employees { get; set; } = new();
    public int TotalCount { get; set; }
}
