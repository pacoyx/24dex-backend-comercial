public static class PayrollPeriodEndpoints
{
    public static RouteGroupBuilder MapPayrollPeriod(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/payroll-periods").WithTags("HR - Payroll");

        group.MapCreatePayrollPeriod();
        group.MapGetPayrollPeriods();

        return group;
    }
}
