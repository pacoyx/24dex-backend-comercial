using Microsoft.EntityFrameworkCore;

public static class GetCustomersEndpoint
{
    public static void MapGetCustomers(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{pageNumber}/{pageSize}", async (int pageNumber, int pageSize, RecepcionDbContext context) =>
        {
            var pagedCustomers = await context.Customers
                .AsNoTracking()
                .OrderBy(c => c.FirtsName)
                .ThenBy(c => c.LastName)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var responsePaginator = new GetCustomersResponseDto(
                await context.Customers.CountAsync(),
                pagedCustomers.Select(c => new GetCustomersResponseDatosDto(
                    c.Id,
                    c.FirtsName,
                    c.LastName,
                    c.Address,
                    c.Phone,
                    c.Email,
                    c.DocPersonal,
                    c.Status
                )).ToList()
            );

            var response = new ApiResponse<GetCustomersResponseDto>
            {
                Success = true,
                Data = responsePaginator,
                StatusCode = StatusCodes.Status200OK,
                Message = "Customers retrieved successfully"
            };
            return Results.Ok(response);
        });
    }
}