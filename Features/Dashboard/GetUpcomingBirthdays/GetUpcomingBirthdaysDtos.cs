public record UpcomingBirthdayResponseDto
{
    public long EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public int DaysUntilBirthday { get; set; }
}
