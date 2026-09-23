using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public static class SneakerReportEndpoint
{
    public static void MapSneakerReport(this IEndpointRouteBuilder app)
    {
        app.MapGet("/sneakers", async (
            [FromQuery] DateTime? dateFrom,
            [FromQuery] DateTime? dateTo,
            [FromQuery] int? branchId,
            [FromQuery] int? brandId,
            [FromQuery] string? ticketStatus,
            [FromQuery] bool? pendingDelivery,
            IValidator<SneakerReportFilter> validator,
            RecepcionDbContext db) =>
        {
            var filter = new SneakerReportFilter
            {
                DateFrom = dateFrom,
                DateTo = dateTo,
                BranchId = branchId,
                BrandId = brandId,
                TicketStatus = ticketStatus,
                PendingDelivery = pendingDelivery
            };

            var validation = await validator.ValidateAsync(filter);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors.Select(e => e.ErrorMessage));

            var query = db.SneakerDetails.AsNoTracking().AsQueryable();

            if (filter.DateFrom.HasValue)
                query = query.Where(s => s.WorkGuideDetail.WorkGuideMain!.FechaOperacion.Date >= filter.DateFrom.Value.Date);

            if (filter.DateTo.HasValue)
                query = query.Where(s => s.WorkGuideDetail.WorkGuideMain!.FechaOperacion.Date <= filter.DateTo.Value.Date);

            if (filter.BranchId.HasValue)
                query = query.Where(s => s.WorkGuideDetail.WorkGuideMain!.BranchId == filter.BranchId.Value);

            if (filter.BrandId.HasValue)
                query = query.Where(s => s.SneakerBrandId == filter.BrandId.Value);

            if (!string.IsNullOrEmpty(filter.TicketStatus))
                query = query.Where(s => s.WorkGuideDetail.EstadoTrabajo == filter.TicketStatus);

            if (filter.PendingDelivery == true)
                query = query.Where(s => s.WorkGuideDetail.EstadoSituacion == "P");

            var rows = await query
                .Select(s => new
                {
                    WorkGuideNumber = s.WorkGuideDetail.WorkGuideMain!.SerieGuia + "-" + s.WorkGuideDetail.WorkGuideMain.NumeroGuia,
                    CustomerName = (s.WorkGuideDetail.WorkGuideMain.Customer!.FirtsName + " " + (s.WorkGuideDetail.WorkGuideMain.Customer.LastName ?? "")).Trim(),
                    Brand = s.Brand != null ? s.Brand.Name : null,
                    s.PrimaryColor,
                    s.HasLaces,
                    s.HasInsoles,
                    DeliveryStatusCode = s.WorkGuideDetail.EstadoSituacion,
                    PhotoCount = s.WorkGuideDetail.ItemInspection != null ? s.WorkGuideDetail.ItemInspection.Photos.Count : 0
                })
                .ToListAsync();

            var response = rows.Select(r => new SneakerReportRowResponse(
                r.WorkGuideNumber,
                r.CustomerName,
                r.Brand,
                r.PrimaryColor,
                r.HasLaces,
                r.HasInsoles,
                MapDeliveryStatus(r.DeliveryStatusCode),
                r.PhotoCount
            )).ToList();

            return Results.Ok(new ApiResponse<List<SneakerReportRowResponse>>
            {
                Success = true,
                Message = "Reporte de zapatillas.",
                StatusCode = StatusCodes.Status200OK,
                Data = response
            });
        });
    }

    private static string MapDeliveryStatus(string code) => code switch
    {
        "P" => "Pending",
        "E" => "Delivered",
        "D" => "Returned",
        _ => "Unknown"
    };
}
