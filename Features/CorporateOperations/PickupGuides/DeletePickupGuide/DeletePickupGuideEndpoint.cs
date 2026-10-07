using Microsoft.EntityFrameworkCore;

public static class DeletePickupGuideEndpoint
{
    public static void MapDeletePickupGuide(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id:long}", async (long id, RecepcionDbContext db) =>
        {
            var guide = await db.PickupGuides.FirstOrDefaultAsync(x => x.Id == id);
            if (guide is null)
            {
                return Results.NotFound();
            }

            if (guide.Status == PickupGuideStatus.Invoiced)
            {
                return Results.BadRequest(new[] { "No se puede eliminar una guía facturada." });
            }

            db.PickupGuides.Remove(guide);
            await db.SaveChangesAsync();

            return Results.Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Guía de recojo eliminada correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = new { guide.Id }
            });
        })
        .WithName("DeletePickupGuide")
        .Produces<ApiResponse<object>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound);
    }
}
