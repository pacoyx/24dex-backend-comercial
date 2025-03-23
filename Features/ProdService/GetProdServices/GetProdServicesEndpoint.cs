using Microsoft.EntityFrameworkCore;

public static class GetProdServicesEndpoint
{
    public static RouteHandlerBuilder MapGetProdServices(this IEndpointRouteBuilder app)
    {
        return app.MapGet("/{pageNumber}/{pageSize}", async (int pageNumber, int pageSize, RecepcionDbContext context) =>
         {
             var prodServices = await context.ProdServices
                 .AsNoTracking()
                 .OrderBy(ps => ps.Name)
                 .Skip((pageNumber - 1) * pageSize)
                 .Take(pageSize)
                 .ToListAsync();

             var responsePaginator = new GetProdServicesResponseDto(
                 await context.ProdServices.CountAsync(),
                 prodServices.Select(ps => new GetProdServicesResponseDatosDto(
                     ps.Id,
                     ps.Name,
                     ps.Description,
                     ps.Price,
                     ps.Status,
                     ps.CatServiceId,
                     ps.IsPeso,
                     ps.IsLavado
                 )).ToList()
             );

             var response = new ApiResponse<GetProdServicesResponseDto>
             {
                 Success = true,
                 Data = responsePaginator,
                 StatusCode = StatusCodes.Status200OK,
                 Message = "ProdServices retrieved successfully"
             };

             return Results.Ok(response);
         });
    }
}