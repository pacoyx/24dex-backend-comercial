using FluentValidation;
using Microsoft.EntityFrameworkCore;

public static class CreateItemInspectionEndpoint
{
    public static void MapCreateItemInspection(this IEndpointRouteBuilder app)
    {
        app.MapPost("/{id:int}/inspection", async (
            int id,
            CreateItemInspectionRequest request,
            IValidator<CreateItemInspectionRequest> validator,
            RecepcionDbContext db) =>
        {
            var validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors.Select(e => e.ErrorMessage));

            var workGuideDetailExists = await db.WorkGuideDetails.AnyAsync(x => x.Id == id);
            if (!workGuideDetailExists)
                return Results.NotFound("Detalle de guía de servicio no encontrado.");

            var inspection = await db.ItemInspections.FirstOrDefaultAsync(x => x.WorkGuideDetailId == id);
            if (inspection is null)
            {
                inspection = new ItemInspection
                {
                    WorkGuideDetailId = id,
                    CreatedAt = DateTime.UtcNow
                };
                db.ItemInspections.Add(inspection);
            }

            inspection.HasStains = request.HasStains;
            inspection.HasTears = request.HasTears;
            inspection.HasFading = request.HasFading;
            inspection.HasMissingParts = request.HasMissingParts;
            inspection.Notes = request.Notes;

            await db.SaveChangesAsync();

            var response = new ItemInspectionResponse(
                inspection.Id, inspection.WorkGuideDetailId, inspection.HasStains, inspection.HasTears,
                inspection.HasFading, inspection.HasMissingParts, inspection.Notes, inspection.CreatedAt);

            return Results.Ok(new ApiResponse<ItemInspectionResponse>
            {
                Success = true,
                Message = "Inspección registrada correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = response
            });
        })
        .RequireAuthorization();
    }
}
