public record RegisterIncidentRequestDto
{
    public DateOnly Date { get; set; }
    public decimal Hours { get; set; }
    public string? Observation { get; set; }
}

public record AttendanceIncidentResponseDto
{
    public long Id { get; set; }
    public long EmployeeId { get; set; }
    public DateOnly Date { get; set; }
    public AttendanceIncidentType Type { get; set; }
    public decimal Hours { get; set; }
    public string? Observation { get; set; }
}
