using Microsoft.EntityFrameworkCore;

public static class GetCollectionSheetsEndpoint
{
    public static void MapGetCollectionSheets(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (RecepcionDbContext db) =>
        {
            var sheets = await db.CollectionSheets
                .AsNoTracking()
                .OrderByDescending(x => x.OpenDate)
                .Select(x => new CollectionSheetResponse(
                    x.Id, x.BranchId, x.Number, x.OpenDate, x.CloseDate,
                    x.TotalAmount, x.Status, x.Remarks))
                .ToListAsync();

            return Results.Ok(new ApiResponse<List<CollectionSheetResponse>>
            {
                Success = true,
                Message = "Listado de planillas de cobranza.",
                StatusCode = StatusCodes.Status200OK,
                Data = sheets
            });
        });
    }
}
