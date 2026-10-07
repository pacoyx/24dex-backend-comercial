using Microsoft.EntityFrameworkCore;

public static class ChangePickupGuideStatusEndpoint
{
    private static readonly Dictionary<PickupGuideStatus, PickupGuideStatus[]> AllowedTransitions = new()
    {
        [PickupGuideStatus.Draft] = new[] { PickupGuideStatus.Collected, PickupGuideStatus.Cancelled },
        [PickupGuideStatus.Collected] = new[] { PickupGuideStatus.Processed, PickupGuideStatus.Cancelled },
        [PickupGuideStatus.Processed] = new[] { PickupGuideStatus.Invoiced, PickupGuideStatus.Cancelled },
        [PickupGuideStatus.Invoiced] = Array.Empty<PickupGuideStatus>(),
        [PickupGuideStatus.Cancelled] = Array.Empty<PickupGuideStatus>()
    };

    public static void MapChangePickupGuideStatus(this IEndpointRouteBuilder app)
    {
        app.MapPost("/{id:long}/status", async (long id, ChangePickupGuideStatusRequest request, RecepcionDbContext db) =>
        {
            var guide = await db.PickupGuides.FirstOrDefaultAsync(x => x.Id == id);
            if (guide is null)
            {
                return Results.NotFound("Guía no encontrada.");
            }

            if (guide.Status == request.Status)
            {
                return Results.BadRequest(new[] { "La guía ya se encuentra en ese estado." });
            }

            if (!AllowedTransitions[guide.Status].Contains(request.Status))
            {
                return Results.BadRequest(new[] { $"No se puede cambiar el estado de '{guide.Status}' a '{request.Status}'." });
            }

            guide.Status = request.Status;
            guide.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));
            await db.SaveChangesAsync();

            return Results.Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Estado de la guía actualizado correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = new { guide.Id, guide.Status }
            });
        })
        .WithName("ChangePickupGuideStatus")
        .Produces<ApiResponse<object>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound);
    }
}
