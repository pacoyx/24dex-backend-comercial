public record TerminateEmployeeRequestDto
{
    public DateTime TerminationDate { get; set; }
    public string? Observation { get; set; }
}
