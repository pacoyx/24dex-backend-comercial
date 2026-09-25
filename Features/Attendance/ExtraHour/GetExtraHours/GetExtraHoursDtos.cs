public record ExtraHourListItemDto
{
    public long Id { get; set; }
    public long EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public decimal Hours { get; set; }
    public string? Observation { get; set; }
    public bool Approved { get; set; }
}
