using FluentValidation;
using Microsoft.EntityFrameworkCore;

public static class UpsertSneakerDetailEndpoint
{
    public static void MapUpsertSneakerDetail(this IEndpointRouteBuilder app)
    {
        app.MapPost("/{id:int}/sneaker", async (
            int id,
            UpsertSneakerDetailRequest request,
            IValidator<UpsertSneakerDetailRequest> validator,
            RecepcionDbContext db) =>
        {
            var validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors.Select(e => e.ErrorMessage));

            var workGuideDetailExists = await db.WorkGuideDetails.AnyAsync(x => x.Id == id);
            if (!workGuideDetailExists)
                return Results.NotFound("Detalle de guía de servicio no encontrado.");

            SneakerBrand? brand = null;
            if (request.BrandId.HasValue)
            {
                brand = await db.SneakerBrands.FirstOrDefaultAsync(x => x.Id == request.BrandId.Value && x.IsActive);
                if (brand is null)
                    return Results.BadRequest("La marca de zapatilla no existe o está inactiva.");
            }

            var sneakerDetail = await db.SneakerDetails.FirstOrDefaultAsync(x => x.WorkGuideDetailId == id);
            if (sneakerDetail is null)
            {
                sneakerDetail = new SneakerDetail
                {
                    WorkGuideDetailId = id,
                    CreatedAt = DateTime.UtcNow
                };
                db.SneakerDetails.Add(sneakerDetail);
            }

            sneakerDetail.SneakerBrandId = request.BrandId;
            sneakerDetail.PrimaryColor = request.PrimaryColor;
            sneakerDetail.SecondaryColor = request.SecondaryColor;
            sneakerDetail.HasLaces = request.HasLaces;
            sneakerDetail.HasInsoles = request.HasInsoles;
            sneakerDetail.Observations = request.Observations;

            await db.SaveChangesAsync();

            var response = new SneakerDetailResponse(
                sneakerDetail.Id, sneakerDetail.WorkGuideDetailId, sneakerDetail.SneakerBrandId, brand?.Name,
                sneakerDetail.PrimaryColor, sneakerDetail.SecondaryColor, sneakerDetail.HasLaces,
                sneakerDetail.HasInsoles, sneakerDetail.Observations, sneakerDetail.CreatedAt);

            return Results.Ok(new ApiResponse<SneakerDetailResponse>
            {
                Success = true,
                Message = "Información de zapatilla guardada correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = response
            });
        })
        .RequireAuthorization();
    }
}
