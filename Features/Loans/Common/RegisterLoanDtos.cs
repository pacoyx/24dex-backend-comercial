public record RegisterLoanRequestDto
{
    public DateTime LoanDate { get; set; }
    public decimal Amount { get; set; }
    public string? Observation { get; set; }
}

public record EmployeeLoanResponseDto
{
    public long Id { get; set; }
    public long EmployeeId { get; set; }
    public LoanType LoanType { get; set; }
    public DateTime LoanDate { get; set; }
    public decimal Amount { get; set; }
    public decimal Balance { get; set; }
    public string? Observation { get; set; }
    public bool IsClosed { get; set; }
}
