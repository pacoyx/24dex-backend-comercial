using FluentValidation;
using Microsoft.AspNetCore.Mvc;

public static class SalesRegisterEndpoints
{
    public static void MapSalesRegister(this IEndpointRouteBuilder app)
    {
        app.MapGet("/sales-register", async (
            [FromQuery] DateTime fromDate,
            [FromQuery] DateTime toDate,
            [FromQuery] int? branchId,
            [FromQuery] int? customerId,
            [FromQuery] InvoiceStatus? status,
            IValidator<SalesRegisterFilter> validator,
            ISalesRegisterReportService reportService) =>
        {
            var (filter, error) = await ValidateFilterAsync(
                fromDate, toDate, branchId, customerId, status, validator);
            if (error is not null)
            {
                return error;
            }

            var rows = await reportService.GetDataAsync(filter!);
            var totals = SalesRegisterReportService.CalculateTotals(rows);

            var response = new ApiResponse<SalesRegisterReportResponse>
            {
                Success = true,
                Message = "Registro de ventas.",
                StatusCode = StatusCodes.Status200OK,
                Data = new SalesRegisterReportResponse
                {
                    Rows = rows,
                    Totals = totals
                }
            };

            return Results.Ok(response);
        });

        app.MapGet("/sales-register/pdf", async (
            [FromQuery] DateTime fromDate,
            [FromQuery] DateTime toDate,
            [FromQuery] int? branchId,
            [FromQuery] int? customerId,
            [FromQuery] InvoiceStatus? status,
            IValidator<SalesRegisterFilter> validator,
            ISalesRegisterReportService reportService) =>
        {
            var (filter, error) = await ValidateFilterAsync(
                fromDate, toDate, branchId, customerId, status, validator);
            if (error is not null)
            {
                return error;
            }

            var pdfBytes = await reportService.GeneratePdfAsync(filter!);
            var fileName = $"RegistroVentas_{DateTime.Now:yyyyMMdd}.pdf";

            return Results.File(pdfBytes, "application/pdf", fileName);
        });

        app.MapGet("/sales-register/excel", async (
            [FromQuery] DateTime fromDate,
            [FromQuery] DateTime toDate,
            [FromQuery] int? branchId,
            [FromQuery] int? customerId,
            [FromQuery] InvoiceStatus? status,
            IValidator<SalesRegisterFilter> validator,
            ISalesRegisterReportService reportService) =>
        {
            var (filter, error) = await ValidateFilterAsync(
                fromDate, toDate, branchId, customerId, status, validator);
            if (error is not null)
            {
                return error;
            }

            var excelBytes = await reportService.GenerateExcelAsync(filter!);
            var fileName = $"RegistroVentas_{DateTime.Now:yyyyMMdd}.xlsx";

            return Results.File(
                excelBytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileName);
        });
    }

    private static async Task<(SalesRegisterFilter? Filter, IResult? Error)> ValidateFilterAsync(
        DateTime fromDate,
        DateTime toDate,
        int? branchId,
        int? customerId,
        InvoiceStatus? status,
        IValidator<SalesRegisterFilter> validator)
    {
        var filter = new SalesRegisterFilter
        {
            FromDate = fromDate,
            ToDate = toDate,
            BranchId = branchId,
            CustomerId = customerId,
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
