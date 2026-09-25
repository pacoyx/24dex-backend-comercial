public class Attendance
{
    public long Id { get; set; }

    public long EmployeeId { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly? CheckIn { get; set; }

    public TimeOnly? CheckOut { get; set; }

    public Employee? Employee { get; set; }
}
