public record EmployeesByAreaResponseDto
{
    public int AreaId { get; set; }
    public string AreaName { get; set; } = string.Empty;
    public int EmployeeCount { get; set; }
}
