public record HrDashboardSummaryResponseDto
{
    public int ActiveEmployees { get; set; }
    public decimal MonthlyPayrollCost { get; set; }
    public decimal PendingLoans { get; set; }
    public decimal ExtraHoursMonth { get; set; }
}
