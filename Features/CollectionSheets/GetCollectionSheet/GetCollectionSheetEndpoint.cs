using Microsoft.EntityFrameworkCore;

public static class GetCollectionSheetEndpoint
{
    public static void MapGetCollectionSheet(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:long}", async (long id, RecepcionDbContext db) =>
        {
            var sheet = await db.CollectionSheets.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (sheet is null)
            {
                return Results.NotFound();
            }

            var response = new CollectionSheetResponse(
                sheet.Id, sheet.BranchId, sheet.Number, sheet.OpenDate, sheet.CloseDate,
                sheet.TotalAmount, sheet.Status, sheet.Remarks);

            return Results.Ok(new ApiResponse<CollectionSheetResponse>
            {
                Success = true,
                Message = "Planilla encontrada.",
                StatusCode = StatusCodes.Status200OK,
                Data = response
            });
        });
    }
}
