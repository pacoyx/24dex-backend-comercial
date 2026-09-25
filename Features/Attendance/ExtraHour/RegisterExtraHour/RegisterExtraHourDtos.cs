public record RegisterExtraHourRequestDto
{
    public DateOnly Date { get; set; }
    public decimal Hours { get; set; }
    public string? Observation { get; set; }
}

public record ExtraHourResponseDto
{
    public long Id { get; set; }
    public long EmployeeId { get; set; }
    public DateOnly Date { get; set; }
    public decimal Hours { get; set; }
    public string? Observation { get; set; }
    public bool Approved { get; set; }
}
