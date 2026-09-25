using Microsoft.EntityFrameworkCore;

public static class GetPendingLoansEndpoint
{
    public static void MapGetPendingLoans(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/hr-dashboard/pending-loans", async (RecepcionDbContext db) =>
        {
            var result = await db.EmployeeLoans.AsNoTracking()
                .Include(x => x.Employee)
                .Where(x => !x.IsClosed && x.Balance > 0)
                .OrderByDescending(x => x.LoanDate)
                .Select(x => new PendingLoanResponseDto
                {
                    Id = x.Id,
                    EmployeeId = x.EmployeeId,
                    EmployeeName = x.Employee!.FirstName + " " + x.Employee.LastName,
                    LoanType = x.LoanType,
                    LoanDate = x.LoanDate,
                    Amount = x.Amount,
                    Balance = x.Balance
                })
                .ToListAsync();

            return Results.Ok(new ApiResponse<IEnumerable<PendingLoanResponseDto>>
            {
                Success = true,
                Data = result,
                StatusCode = StatusCodes.Status200OK,
                Message = "Préstamos pendientes obtenidos correctamente"
            });
        })
        .WithName("GetPendingLoansDashboard")
        .WithTags("HR - Dashboard")
        .Produces<ApiResponse<IEnumerable<PendingLoanResponseDto>>>(StatusCodes.Status200OK)
        .RequireAuthorization();
    }
}
