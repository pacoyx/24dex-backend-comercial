using FluentValidation;
using Microsoft.AspNetCore.Mvc;

public static class ExpenseReportsEndpoints
{
    public static void MapExpenseReports(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/expense-reports").WithTags("ExpenseReports").RequirePermission(PermissionCodes.Expenses);

        MapByPeriod(group);
        MapWithSupportingDocuments(group);
        MapByCategory(group);
        MapByBranch(group);
    }

    private static void MapByPeriod(IEndpointRouteBuilder group)
    {
        group.MapGet("/by-period", async (
            [FromQuery] DateTime fromDate,
            [FromQuery] DateTime toDate,
            [FromQuery] int? branchSalesId,
            [FromQuery] int? expenseCategoryId,
            [FromQuery] ExpenseSourceType? sourceType,
            IValidator<ExpenseReportFilter> validator,
            IExpenseReportService reportService) =>
        {
            var (filter, error) = await ValidateFilterAsync(fromDate, toDate, branchSalesId, expenseCategoryId, sourceType, validator);
            if (error is not null)
            {
                return error;
            }

            var rows = await reportService.GetByPeriodAsync(filter!);
            var totals = ExpenseReportService.CalculateTotals(rows);

            return Results.Ok(new ApiResponse<ExpenseDetailReportResponse>
            {
                Success = true,
                Message = "Gastos por periodo.",
                StatusCode = StatusCodes.Status200OK,
                Data = new ExpenseDetailReportResponse { Rows = rows, Totals = totals }
            });
        });

        group.MapGet("/by-period/pdf", async (
            [FromQuery] DateTime fromDate,
            [FromQuery] DateTime toDate,
            [FromQuery] int? branchSalesId,
            [FromQuery] int? expenseCategoryId,
            [FromQuery] ExpenseSourceType? sourceType,
            IValidator<ExpenseReportFilter> validator,
            IExpenseReportService reportService) =>
        {
            var (filter, error) = await ValidateFilterAsync(fromDate, toDate, branchSalesId, expenseCategoryId, sourceType, validator);
            if (error is not null)
            {
                return error;
            }

            var rows = await reportService.GetByPeriodAsync(filter!);
            var pdfBytes = await reportService.GenerateDetailPdfAsync("GASTOS POR PERIODO", rows, filter!);
            return Results.File(pdfBytes, "application/pdf", $"GastosPorPeriodo_{DateTime.Now:yyyyMMdd}.pdf");
        });

        group.MapGet("/by-period/excel", async (
            [FromQuery] DateTime fromDate,
            [FromQuery] DateTime toDate,
            [FromQuery] int? branchSalesId,
            [FromQuery] int? expenseCategoryId,
            [FromQuery] ExpenseSourceType? sourceType,
            IValidator<ExpenseReportFilter> validator,
            IExpenseReportService reportService) =>
        {
            var (filter, error) = await ValidateFilterAsync(fromDate, toDate, branchSalesId, expenseCategoryId, sourceType, validator);
            if (error is not null)
            {
                return error;
            }

            var rows = await reportService.GetByPeriodAsync(filter!);
            var excelBytes = await reportService.GenerateDetailExcelAsync(rows);
            return Results.File(
                excelBytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"GastosPorPeriodo_{DateTime.Now:yyyyMMdd}.xlsx");
        });
    }

    private static void MapWithSupportingDocuments(IEndpointRouteBuilder group)
    {
        group.MapGet("/with-supporting-documents", async (
            [FromQuery] DateTime fromDate,
            [FromQuery] DateTime toDate,
            [FromQuery] int? branchSalesId,
            [FromQuery] int? expenseCategoryId,
            [FromQuery] ExpenseSourceType? sourceType,
            IValidator<ExpenseReportFilter> validator,
            IExpenseReportService reportService) =>
        {
            var (filter, error) = await ValidateFilterAsync(fromDate, toDate, branchSalesId, expenseCategoryId, sourceType, validator);
            if (error is not null)
            {
                return error;
            }

            var rows = await reportService.GetWithSupportingDocumentsAsync(filter!);
            var totals = ExpenseReportService.CalculateTotals(rows);

            return Results.Ok(new ApiResponse<ExpenseDetailReportResponse>
            {
                Success = true,
                Message = "Gastos con documento de sustento.",
                StatusCode = StatusCodes.Status200OK,
                Data = new ExpenseDetailReportResponse { Rows = rows, Totals = totals }
            });
        });

        group.MapGet("/with-supporting-documents/pdf", async (
            [FromQuery] DateTime fromDate,
            [FromQuery] DateTime toDate,
            [FromQuery] int? branchSalesId,
            [FromQuery] int? expenseCategoryId,
            [FromQuery] ExpenseSourceType? sourceType,
            IValidator<ExpenseReportFilter> validator,
            IExpenseReportService reportService) =>
        {
            var (filter, error) = await ValidateFilterAsync(fromDate, toDate, branchSalesId, expenseCategoryId, sourceType, validator);
            if (error is not null)
            {
                return error;
            }

            var rows = await reportService.GetWithSupportingDocumentsAsync(filter!);
            var pdfBytes = await reportService.GenerateDetailPdfAsync("GASTOS CON DOCUMENTO DE SUSTENTO", rows, filter!);
            return Results.File(pdfBytes, "application/pdf", $"GastosConDocumento_{DateTime.Now:yyyyMMdd}.pdf");
        });

        group.MapGet("/with-supporting-documents/excel", async (
            [FromQuery] DateTime fromDate,
            [FromQuery] DateTime toDate,
            [FromQuery] int? branchSalesId,
            [FromQuery] int? expenseCategoryId,
            [FromQuery] ExpenseSourceType? sourceType,
            IValidator<ExpenseReportFilter> validator,
            IExpenseReportService reportService) =>
        {
            var (filter, error) = await ValidateFilterAsync(fromDate, toDate, branchSalesId, expenseCategoryId, sourceType, validator);
            if (error is not null)
            {
                return error;
            }

            var rows = await reportService.GetWithSupportingDocumentsAsync(filter!);
            var excelBytes = await reportService.GenerateDetailExcelAsync(rows);
            return Results.File(
                excelBytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"GastosConDocumento_{DateTime.Now:yyyyMMdd}.xlsx");
        });
    }

    private static void MapByCategory(IEndpointRouteBuilder group)
    {
        group.MapGet("/by-category", async (
            [FromQuery] DateTime fromDate,
            [FromQuery] DateTime toDate,
            [FromQuery] int? branchSalesId,
            [FromQuery] int? expenseCategoryId,
            [FromQuery] ExpenseSourceType? sourceType,
            IValidator<ExpenseReportFilter> validator,
            IExpenseReportService reportService) =>
        {
            var (filter, error) = await ValidateFilterAsync(fromDate, toDate, branchSalesId, expenseCategoryId, sourceType, validator);
            if (error is not null)
            {
                return error;
            }

            var rows = await reportService.GetByCategoryAsync(filter!);
            var totals = ExpenseReportService.CalculateAggregateTotals(rows);

            return Results.Ok(new ApiResponse<ExpenseAggregateReportResponse>
            {
                Success = true,
                Message = "Gastos por categoría.",
                StatusCode = StatusCodes.Status200OK,
                Data = new ExpenseAggregateReportResponse { Rows = rows, Totals = totals }
            });
        });

        group.MapGet("/by-category/pdf", async (
            [FromQuery] DateTime fromDate,
            [FromQuery] DateTime toDate,
            [FromQuery] int? branchSalesId,
            [FromQuery] int? expenseCategoryId,
            [FromQuery] ExpenseSourceType? sourceType,
            IValidator<ExpenseReportFilter> validator,
            IExpenseReportService reportService) =>
        {
            var (filter, error) = await ValidateFilterAsync(fromDate, toDate, branchSalesId, expenseCategoryId, sourceType, validator);
            if (error is not null)
            {
                return error;
            }

            var rows = await reportService.GetByCategoryAsync(filter!);
            var pdfBytes = await reportService.GenerateAggregatePdfAsync("GASTOS POR CATEGORÍA", "Categoría", rows, filter!);
            return Results.File(pdfBytes, "application/pdf", $"GastosPorCategoria_{DateTime.Now:yyyyMMdd}.pdf");
        });

        group.MapGet("/by-category/excel", async (
            [FromQuery] DateTime fromDate,
            [FromQuery] DateTime toDate,
            [FromQuery] int? branchSalesId,
            [FromQuery] int? expenseCategoryId,
            [FromQuery] ExpenseSourceType? sourceType,
            IValidator<ExpenseReportFilter> validator,
            IExpenseReportService reportService) =>
        {
            var (filter, error) = await ValidateFilterAsync(fromDate, toDate, branchSalesId, expenseCategoryId, sourceType, validator);
            if (error is not null)
            {
                return error;
            }

            var rows = await reportService.GetByCategoryAsync(filter!);
            var excelBytes = await reportService.GenerateAggregateExcelAsync("Categoría", rows);
            return Results.File(
                excelBytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"GastosPorCategoria_{DateTime.Now:yyyyMMdd}.xlsx");
        });
    }

    private static void MapByBranch(IEndpointRouteBuilder group)
    {
        group.MapGet("/by-branch", async (
            [FromQuery] DateTime fromDate,
            [FromQuery] DateTime toDate,
            [FromQuery] int? branchSalesId,
            [FromQuery] int? expenseCategoryId,
            [FromQuery] ExpenseSourceType? sourceType,
            IValidator<ExpenseReportFilter> validator,
            IExpenseReportService reportService) =>
        {
            var (filter, error) = await ValidateFilterAsync(fromDate, toDate, branchSalesId, expenseCategoryId, sourceType, validator);
            if (error is not null)
            {
                return error;
            }

            var rows = await reportService.GetByBranchAsync(filter!);
            var totals = ExpenseReportService.CalculateAggregateTotals(rows);

            return Results.Ok(new ApiResponse<ExpenseAggregateReportResponse>
            {
                Success = true,
                Message = "Gastos por sucursal.",
                StatusCode = StatusCodes.Status200OK,
                Data = new ExpenseAggregateReportResponse { Rows = rows, Totals = totals }
            });
        });

        group.MapGet("/by-branch/pdf", async (
            [FromQuery] DateTime fromDate,
            [FromQuery] DateTime toDate,
            [FromQuery] int? branchSalesId,
            [FromQuery] int? expenseCategoryId,
            [FromQuery] ExpenseSourceType? sourceType,
            IValidator<ExpenseReportFilter> validator,
            IExpenseReportService reportService) =>
        {
            var (filter, error) = await ValidateFilterAsync(fromDate, toDate, branchSalesId, expenseCategoryId, sourceType, validator);
            if (error is not null)
            {
                return error;
            }

            var rows = await reportService.GetByBranchAsync(filter!);
            var pdfBytes = await reportService.GenerateAggregatePdfAsync("GASTOS POR SUCURSAL", "Sucursal", rows, filter!);
            return Results.File(pdfBytes, "application/pdf", $"GastosPorSucursal_{DateTime.Now:yyyyMMdd}.pdf");
        });

        group.MapGet("/by-branch/excel", async (
            [FromQuery] DateTime fromDate,
            [FromQuery] DateTime toDate,
            [FromQuery] int? branchSalesId,
            [FromQuery] int? expenseCategoryId,
            [FromQuery] ExpenseSourceType? sourceType,
            IValidator<ExpenseReportFilter> validator,
            IExpenseReportService reportService) =>
        {
            var (filter, error) = await ValidateFilterAsync(fromDate, toDate, branchSalesId, expenseCategoryId, sourceType, validator);
            if (error is not null)
            {
                return error;
            }

            var rows = await reportService.GetByBranchAsync(filter!);
            var excelBytes = await reportService.GenerateAggregateExcelAsync("Sucursal", rows);
            return Results.File(
                excelBytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"GastosPorSucursal_{DateTime.Now:yyyyMMdd}.xlsx");
        });
    }

    private static async Task<(ExpenseReportFilter? Filter, IResult? Error)> ValidateFilterAsync(
        DateTime fromDate,
        DateTime toDate,
        int? branchSalesId,
        int? expenseCategoryId,
        ExpenseSourceType? sourceType,
        IValidator<ExpenseReportFilter> validator)
    {
        var filter = new ExpenseReportFilter
        {
            FromDate = fromDate,
            ToDate = toDate,
            BranchSalesId = branchSalesId,
            ExpenseCategoryId = expenseCategoryId,
            SourceType = sourceType
        };

        var validation = await validator.ValidateAsync(filter);
        if (!validation.IsValid)
        {
            return (null, Results.BadRequest(validation.Errors.Select(e => e.ErrorMessage)));
        }

        return (filter, null);
    }
}
