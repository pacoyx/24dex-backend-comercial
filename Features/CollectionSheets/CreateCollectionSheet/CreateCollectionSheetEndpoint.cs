using FluentValidation;
using Microsoft.EntityFrameworkCore;

public static class CreateCollectionSheetEndpoint
{
    public static void MapCreateCollectionSheet(this IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (
            CreateCollectionSheetRequest request,
            IValidator<CreateCollectionSheetRequest> validator,
            RecepcionDbContext db) =>
        {
            var validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return Results.BadRequest(validation.Errors.Select(e => e.ErrorMessage));
            }

            var numberInUse = await db.CollectionSheets.AnyAsync(x => x.Number == request.Number);
            if (numberInUse)
            {
                return Results.BadRequest("Ya existe una planilla registrada con ese número.");
            }

            var sheet = new CollectionSheet
            {
                BranchId = request.BranchId,
                Number = request.Number,
                Remarks = request.Remarks,
                OpenDate = DateTime.UtcNow,
                TotalAmount = 0,
                Status = CollectionSheetStatus.Open
            };

            await db.CollectionSheets.AddAsync(sheet);
            await db.SaveChangesAsync();

            var response = new CollectionSheetResponse(
                sheet.Id, sheet.BranchId, sheet.Number, sheet.OpenDate, sheet.CloseDate,
                sheet.TotalAmount, sheet.Status, sheet.Remarks);

            return Results.Created($"/api/collection-sheets/{sheet.Id}", new ApiResponse<CollectionSheetResponse>
            {
                Success = true,
                Message = "Planilla de cobranza creada correctamente.",
                StatusCode = StatusCodes.Status201Created,
                Data = response
            });
        })
        .RequireAuthorization();
    }
}
