public class EmployeeSalaryHistory
{
    public long Id { get; set; }

    public long EmployeeId { get; set; }

    public decimal Salary { get; set; }

    public DateTime EffectiveDate { get; set; }

    public Employee? Employee { get; set; }
}
