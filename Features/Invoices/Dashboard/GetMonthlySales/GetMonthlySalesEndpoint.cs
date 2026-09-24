using System.Globalization;
using Microsoft.EntityFrameworkCore;

public static class GetMonthlySalesEndpoint
{
    public static void MapGetMonthlySales(this IEndpointRouteBuilder app)
    {
        app.MapGet("/monthly-sales", async (RecepcionDbContext db) =>
        {
            var today = DateTime.UtcNow.Date;
            var start = new DateTime(today.Year, today.Month, 1).AddMonths(-11);

            var invoices = await db.Invoices
                .AsNoTracking()
                .Where(x => x.IssueDate >= start && x.Status != InvoiceStatus.Cancelled)
                .Select(x => new { x.IssueDate, x.Total })
                .ToListAsync();

            var cultura = new CultureInfo("es-ES");
            var data = new List<MonthlySalesResponseDto>();
            for (var month = start; month <= today; month = month.AddMonths(1))
            {
                var amount = invoices
                    .Where(x => x.IssueDate.Year == month.Year && x.IssueDate.Month == month.Month)
                    .Sum(x => x.Total);

                var label = cultura.TextInfo.ToTitleCase(month.ToString("MMM", cultura)).TrimEnd('.');
                data.Add(new MonthlySalesResponseDto(label, amount));
            }

            return Results.Ok(new ApiResponse<List<MonthlySalesResponseDto>>
            {
                Success = true,
                Message = "Facturación por mes.",
                StatusCode = StatusCodes.Status200OK,
                Data = data
            });
        });
    }
}
