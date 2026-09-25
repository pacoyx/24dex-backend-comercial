public record LoanReportItemDto
{
    public long Id { get; set; }
    public long EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public DateTime LoanDate { get; set; }
    public decimal Amount { get; set; }
    public decimal Balance { get; set; }
    public bool IsClosed { get; set; }
    public string? Observation { get; set; }
}
