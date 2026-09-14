using Microsoft.EntityFrameworkCore;

public static class CloseCollectionSheetEndpoint
{
    public static void MapCloseCollectionSheet(this IEndpointRouteBuilder app)
    {
        app.MapPost("/{id:long}/close", async (long id, RecepcionDbContext db) =>
        {
            var sheet = await db.CollectionSheets.FirstOrDefaultAsync(x => x.Id == id);
            if (sheet is null)
            {
                return Results.NotFound();
            }

            if (sheet.Status == CollectionSheetStatus.Closed)
            {
                return Results.BadRequest("La planilla ya se encuentra cerrada.");
            }

            sheet.Status = CollectionSheetStatus.Closed;
            sheet.CloseDate = DateTime.UtcNow;
            await db.SaveChangesAsync();

            return Results.Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Planilla cerrada correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = new { sheet.Id, sheet.Status, sheet.CloseDate }
            });
        })
        .RequireAuthorization();
    }
}
