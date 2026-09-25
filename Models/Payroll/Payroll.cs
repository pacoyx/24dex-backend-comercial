public class Payroll
{
    public long Id { get; set; }

    public long PayrollPeriodId { get; set; }

    public long EmployeeId { get; set; }

    public decimal GrossAmount { get; set; }

    public decimal Discounts { get; set; }

    public decimal NetAmount { get; set; }

    public DateTime GeneratedAt { get; set; }

    public PayrollPeriod? PayrollPeriod { get; set; }

    public Employee? Employee { get; set; }

    public List<PayrollDetail>? PayrollDetails { get; set; }
}
