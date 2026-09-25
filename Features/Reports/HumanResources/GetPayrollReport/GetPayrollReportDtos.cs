public record PayrollReportItemDto
{
    public long PayrollId { get; set; }
    public long PayrollPeriodId { get; set; }
    public string PayrollPeriodName { get; set; } = string.Empty;
    public long EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public decimal GrossAmount { get; set; }
    public decimal Discounts { get; set; }
    public decimal NetAmount { get; set; }
    public DateTime GeneratedAt { get; set; }
}
