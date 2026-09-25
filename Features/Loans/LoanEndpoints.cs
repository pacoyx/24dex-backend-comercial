public static class LoanEndpoints
{
    public static void MapLoan(this IEndpointRouteBuilder app)
    {
        var employeesGroup = app.MapGroup("/api/employees").WithTags("HR - Loans");
        employeesGroup.MapRegisterLoan();
        employeesGroup.MapRegisterSalaryAdvance();

        var loansGroup = app.MapGroup("/api/loans").WithTags("HR - Loans");
        loansGroup.MapGetLoans();
    }
}
