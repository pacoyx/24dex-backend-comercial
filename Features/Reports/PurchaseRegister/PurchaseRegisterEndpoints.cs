using FluentValidation;
using Microsoft.AspNetCore.Mvc;

public static class PurchaseRegisterEndpoints
{
    public static void MapPurchaseRegister(this IEndpointRouteBuilder app)
    {
        app.MapGet("/purchase-register", async (
            [FromQuery] DateTime fromDate,
            [FromQuery] DateTime toDate,
            [FromQuery] int? supplierId,
            [FromQuery] PurchaseInvoiceStatus? status,
            IValidator<PurchaseRegisterFilter> validator,
            IPurchaseRegisterReportService reportService) =>
        {
            var (filter, error) = await ValidateFilterAsync(
                fromDate, toDate, supplierId, status, validator);
            if (error is not null)
            {
                return error;
            }

            var rows = await reportService.GetDataAsync(filter!);
            var totals = PurchaseRegisterReportService.CalculateTotals(rows);

            var response = new ApiResponse<PurchaseRegisterReportResponse>
            {
                Success = true,
                Message = "Registro de compras.",
                StatusCode = StatusCodes.Status200OK,
                Data = new PurchaseRegisterReportResponse
                {
                    Rows = rows,
                    Totals = totals
                }
            };

            return Results.Ok(response);
        });

        app.MapGet("/purchase-register/pdf", async (
            [FromQuery] DateTime fromDate,
            [FromQuery] DateTime toDate,
            [FromQuery] int? supplierId,
            [FromQuery] PurchaseInvoiceStatus? status,
            IValidator<PurchaseRegisterFilter> validator,
            IPurchaseRegisterReportService reportService) =>
        {
            var (filter, error) = await ValidateFilterAsync(
                fromDate, toDate, supplierId, status, validator);
            if (error is not null)
            {
                return error;
            }

            var pdfBytes = await reportService.GeneratePdfAsync(filter!);
            var fileName = $"RegistroCompras_{DateTime.Now:yyyyMMdd}.pdf";

            return Results.File(pdfBytes, "application/pdf", fileName);
        });

        app.MapGet("/purchase-register/excel", async (
            [FromQuery] DateTime fromDate,
            [FromQuery] DateTime toDate,
            [FromQuery] int? supplierId,
            [FromQuery] PurchaseInvoiceStatus? status,
            IValidator<PurchaseRegisterFilter> validator,
            IPurchaseRegisterReportService reportService) =>
        {
            var (filter, error) = await ValidateFilterAsync(
                fromDate, toDate, supplierId, status, validator);
            if (error is not null)
            {
                return error;
            }

            var excelBytes = await reportService.GenerateExcelAsync(filter!);
            var fileName = $"RegistroCompras_{DateTime.Now:yyyyMMdd}.xlsx";

            return Results.File(
                excelBytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileName);
        });
    }

    private static async Task<(PurchaseRegisterFilter? Filter, IResult? Error)> ValidateFilterAsync(
        DateTime fromDate,
        DateTime toDate,
        int? supplierId,
        PurchaseInvoiceStatus? status,
        IValidator<PurchaseRegisterFilter> validator)
    {
        var filter = new PurchaseRegisterFilter
        {
            FromDate = fromDate,
            ToDate = toDate,
            SupplierId = supplierId,
            Status = status
        };

        var validation = await validator.ValidateAsync(filter);
        if (!validation.IsValid)
        {
            return (null, Results.BadRequest(validation.Errors.Select(e => e.ErrorMessage)));
        }

        return (filter, null);
    }
}
