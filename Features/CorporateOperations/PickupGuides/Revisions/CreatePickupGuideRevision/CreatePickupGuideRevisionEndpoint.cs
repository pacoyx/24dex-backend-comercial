using FluentValidation;
using Microsoft.EntityFrameworkCore;

public static class CreatePickupGuideRevisionEndpoint
{
    public static void MapCreatePickupGuideRevision(this IEndpointRouteBuilder app)
    {
        app.MapPost("/{id:long}/revisions", async (
            long id,
            CreatePickupGuideRevisionRequest request,
            IValidator<CreatePickupGuideRevisionRequest> validator,
            RecepcionDbContext db) =>
        {
            var validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return Results.BadRequest(validation.Errors.Select(e => e.ErrorMessage));
            }

            var guide = await db.PickupGuides
                .Include(x => x.Details).ThenInclude(d => d.ServiceItem)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (guide is null)
            {
                return Results.NotFound("Guía no encontrada.");
            }

            // La revisión de planta corrige cantidades mientras la guía sigue en Draft;
            // una vez Collected, las cantidades quedan congeladas en la constancia emitida.
            if (guide.Status != PickupGuideStatus.Draft)
            {
                return Results.BadRequest(new[] { $"Solo se pueden registrar revisiones sobre guías en estado 'Draft'. Estado actual: '{guide.Status}'." });
            }

            var employeeExists = await db.Employees.AnyAsync(x => x.Id == request.EmployeeId && x.IsActive);
            if (!employeeExists)
            {
                return Results.BadRequest(new[] { "Empleado no encontrado o inactivo." });
            }

            var detailIds = request.Details.Select(d => d.PickupGuideDetailId).ToList();
            var missingDetailIds = detailIds.Except(guide.Details.Select(d => d.Id)).ToList();
            if (missingDetailIds.Count > 0)
            {
                return Results.BadRequest(new[] { $"Los siguientes detalles no pertenecen a esta guía: {string.Join(", ", missingDetailIds)}" });
            }

            DateTime fechaOperacion = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));

            await using var transaction = await db.Database.BeginTransactionAsync();

            var revision = new PickupGuideRevision
            {
                PickupGuideId = id,
                EmployeeId = request.EmployeeId,
                Observation = request.Observation ?? string.Empty,
                RevisionDate = fechaOperacion
            };

            foreach (var detailRequest in request.Details)
            {
                var detail = guide.Details.First(d => d.Id == detailRequest.PickupGuideDetailId);
                var isPricedByWeight = detail.ServiceItem.IsPeso == "S";
                var originalQuantity = isPricedByWeight ? detail.Weight ?? 0 : detail.Quantity;

                revision.DetailRevisions.Add(new PickupGuideDetailRevision
                {
                    PickupGuideDetailId = detail.Id,
                    OriginalQuantity = originalQuantity,
                    NewQuantity = detailRequest.NewQuantity,
                    Reason = detailRequest.Reason
                });

                if (isPricedByWeight)
                {
                    detail.Weight = detailRequest.NewQuantity;
                }
                else
                {
                    detail.Quantity = detailRequest.NewQuantity;
                }

                detail.Total = detailRequest.NewQuantity * detail.UnitPrice;
            }

            guide.TotalAmount = guide.Details.Sum(d => d.Total);
            guide.UpdatedAt = fechaOperacion;

            await db.PickupGuideRevisions.AddAsync(revision);
            await db.SaveChangesAsync();
            await transaction.CommitAsync();

            await db.Entry(revision).Collection(x => x.DetailRevisions).Query()
                .Include(x => x.PickupGuideDetail).ThenInclude(d => d.ServiceItem)
                .LoadAsync();
            await db.Entry(revision).Reference(x => x.Employee).LoadAsync();

            var response = new ApiResponse<PickupGuideRevisionResponse>
            {
                Success = true,
                Message = "Revisión registrada correctamente.",
                StatusCode = StatusCodes.Status201Created,
                Data = PickupGuideRevisionsEndpoints.ToResponse(revision)
            };

            return Results.Created($"/api/pickup-guides/{id}/revisions", response);
        })
        .WithName("CreatePickupGuideRevision")
        .Produces<ApiResponse<PickupGuideRevisionResponse>>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound);
    }
}
