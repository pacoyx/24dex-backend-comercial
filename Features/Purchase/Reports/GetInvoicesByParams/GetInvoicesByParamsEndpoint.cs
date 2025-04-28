using Microsoft.AspNetCore.Mvc;

public static class GetInvoicesByParamsEndpoint
{
    public static IEndpointRouteBuilder MapGetInvoicesByParamsEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/byMonth/{pageNumber}/{pageSize}/{month}/{year}", async (IGetInvoicesByParamsService service, int pageNumber, int pageSize, int year, int month) =>
        {
            var response = await service.GetInvoicesByMonth(pageNumber, pageSize, month, year);
            return Results.Ok(new ApiResponse<InvoiceListPaginatorResponseDto>
            {
                Success = true,
                Data = response,
                StatusCode = StatusCodes.Status200OK,
                Message = "get invoices by month"
            });

        })  
            .WithName("GetInvoicesByMonth")
            .Produces<InvoiceListPaginatorResponseDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        routes.MapGet("/byDate/{pageNumber}/{pageSize}", async (IGetInvoicesByParamsService service, int pageNumber, int pageSize, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate) =>
        {
            var response = await service.GetInvoicesByDate(pageNumber, pageSize, startDate, endDate);
            return Results.Ok(new ApiResponse<InvoiceListPaginatorResponseDto>
            {
                Success = true,
                Data = response,
                StatusCode = StatusCodes.Status200OK,
                Message = "get invoices by date"
            });
        })
            .WithName("GetInvoicesByDate")
            .Produces<InvoiceListPaginatorResponseDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        routes.MapGet("/bySupplier/{pageNumber}/{pageSize}/{supplierId}", async (IGetInvoicesByParamsService service, int pageNumber, int pageSize, int supplierId) =>
        {
            var response = await service.GetInvoicesBySupplier(pageNumber, pageSize, supplierId);
            return Results.Ok(new ApiResponse<InvoiceListPaginatorResponseDto>
            {
                Success = true,
                Data = response,
                StatusCode = StatusCodes.Status200OK,
                Message = "get invoices by supplier"
            });
        })
            .WithName("GetInvoicesBySupplier")
            .Produces<InvoiceListPaginatorResponseDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        routes.MapGet("/byProduct/{pageNumber}/{pageSize}/{productNamePatron}", async (IGetInvoicesByParamsService service, int pageNumber, int pageSize, string productNamePatron) =>
        {
            var response = await service.GetInvoicesByProduct(pageNumber, pageSize, productNamePatron);
            return Results.Ok(new ApiResponse<InvoiceListPaginatorResponseDto>
            {
                Success = true,
                Data = response,
                StatusCode = StatusCodes.Status200OK,
                Message = "get invoices by product"
            });
        })
            .WithName("GetInvoicesByProduct")
            .Produces<InvoiceListPaginatorResponseDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        return routes;
    }
}