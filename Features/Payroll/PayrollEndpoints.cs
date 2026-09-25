public static class PayrollEndpoints
{
    public static void MapPayroll(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/payroll-periods").WithTags("HR - Payroll");

        group.MapGeneratePayroll();
        group.MapGetPayroll();
    }
}
