public static class GetSupplierEndpoint
{
    public static void MapGetSupplier(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", async (int id, IGetSupplierService getSupplierService) =>
        {
            var supplier = await getSupplierService.GetSupplierAsync(id);
            if (supplier == null)
            {
                return Results.NotFound(new ApiResponse<GetSupplierResponseDto>
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Supplier not found"
                });
            }

            var response = new ApiResponse<GetSupplierResponseDto>
            {
                Success = true,
                Data = supplier,
                StatusCode = StatusCodes.Status200OK,
                Message = "get suppliers"
            };
            
            return Results.Ok(response);
        })
        .WithName("GetSupplier")
        .Produces<GetSupplierResponseDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .WithTags("Suppliers");

        app.MapGet("/", async (IGetSupplierService getSupplierService) =>
        {
            var supplier = await getSupplierService.GetSuppliersAsync();
            if (supplier == null || !supplier.Any())
            {
                return Results.NotFound(new ApiResponse<IEnumerable<GetSupplierResponseDto>>
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "No suppliers found"
                });
            }

            var response = new ApiResponse<IEnumerable<GetSupplierResponseDto>>
            {
                Success = true,
                Data = supplier,
                StatusCode = StatusCodes.Status200OK,
                Message = "get suppliers"
            };

            return Results.Ok(response);

        })
        .WithName("GetSuppliers")
        .Produces<GetSupplierResponseDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .WithTags("Suppliers");


        app.MapGet("/search/paginator/{pageNumber:int}/{pageSize:int}", async (int pageNumber, int pageSize, string nameSupplier, IGetSupplierService getSupplierService) =>
        {
            var suppliers = await getSupplierService.GetSuppliersPaginatorAsync(pageNumber, pageSize, nameSupplier);
            var response = new ApiResponse<GetSupplierResponsePaginatorDto>
            {
                Success = true,
                Data = suppliers,
                StatusCode = StatusCodes.Status200OK,
                Message = "get suppliers paginator"
            };

            return Results.Ok(response);
        })
        .WithName("GetSuppliersPaginator")
        .Produces<GetSupplierResponsePaginatorDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .WithTags("Suppliers");


        app.MapGet("/search/byPatron/{patronName}", async (IGetSupplierService getSupplierService, string patronName) =>
        {

            if (string.IsNullOrWhiteSpace(patronName))
            {
                return Results.BadRequest("The name is required");
            }

            var supplier = await getSupplierService.GetSuppliersByPatronAsync(patronName);
            var response = new ApiResponse<IEnumerable<GetSupplierSearchPatronResponseDto>>
            {
                Success = true,
                Data = supplier,
                StatusCode = StatusCodes.Status200OK,
                Message = "get suppliers by patron"
            };

            return Results.Ok(response);

        })
        .WithName("GetSuppliersByPatron")
        .Produces<GetSupplierSearchPatronResponseDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .WithTags("Suppliers");
    }
}