public record UpdateEmployeeRequestDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string? Address { get; set; }
    public int AreaId { get; set; }
    public int PositionId { get; set; }
    public EmployeeContractType ContractType { get; set; }
    public decimal BaseSalary { get; set; }
}
