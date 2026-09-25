public record PendingLoanResponseDto
{
    public long Id { get; set; }
    public long EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public LoanType LoanType { get; set; }
    public DateTime LoanDate { get; set; }
    public decimal Amount { get; set; }
    public decimal Balance { get; set; }
}
