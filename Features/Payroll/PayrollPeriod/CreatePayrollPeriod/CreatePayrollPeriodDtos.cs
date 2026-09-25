public record CreatePayrollPeriodRequestDto
{
    public string Name { get; set; } = string.Empty;
    public PayrollFrequency Frequency { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}

public record PayrollPeriodResponseDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public PayrollFrequency Frequency { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public bool IsClosed { get; set; }
}
