using Microsoft.EntityFrameworkCore;

public static class GetInvoiceBranchSalesEndpoint
{
    public static void MapGetInvoiceBranchSales(this IEndpointRouteBuilder app)
    {
        app.MapGet("/branch-sales", async (RecepcionDbContext db) =>
        {
            var today = DateTime.UtcNow.Date;
            var monthStart = new DateTime(today.Year, today.Month, 1);
            var monthEnd = monthStart.AddMonths(1);

            var totalsByBranch = await db.Invoices
                .AsNoTracking()
                .Where(x => x.IssueDate >= monthStart && x.IssueDate < monthEnd && x.Status != InvoiceStatus.Cancelled)
                .GroupBy(x => x.BranchId)
                .Select(g => new { BranchId = g.Key, Amount = g.Sum(x => x.Total) })
                .OrderByDescending(x => x.Amount)
                .ToListAsync();

            var branchIds = totalsByBranch.Select(x => x.BranchId).ToList();
            var branchNames = await db.BranchSales
                .AsNoTracking()
                .Where(x => branchIds.Contains(x.Id))
                .ToDictionaryAsync(x => x.Id, x => x.Description);

            var response = totalsByBranch
                .Select(x => new InvoiceBranchSalesResponseDto(
                    branchNames.TryGetValue(x.BranchId, out var name) ? name : string.Empty,
                    x.Amount))
                .ToList();

            return Results.Ok(new ApiResponse<List<InvoiceBranchSalesResponseDto>>
            {
                Success = true,
                Message = "Facturación por sucursal.",
                StatusCode = StatusCodes.Status200OK,
                Data = response
            });
        });
    }
}
