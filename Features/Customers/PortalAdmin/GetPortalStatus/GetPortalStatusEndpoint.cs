using Microsoft.EntityFrameworkCore;

public static class GetPortalStatusEndpoint
{
    public static void MapGetPortalStatus(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{customerId:int}/portal-status", async (int customerId, RecepcionDbContext db) =>
        {
            var customer = await db.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == customerId);
            if (customer is null)
            {
                return Results.NotFound("Cliente no encontrado.");
            }

            var users = await db.CustomerUsers
                .AsNoTracking()
                .Include(x => x.CustomerRole)
                .Where(x => x.CustomerId == customerId)
                .ToListAsync();

            var response = new PortalStatusResponse(
                customer.PortalEnabled,
                users.Count,
                users.Count(x => x.IsActive),
                users.Any(x => x.IsActive && x.CustomerRole.Name == CustomerRoleNames.AdministradorEmpresa));

            return Results.Ok(new ApiResponse<PortalStatusResponse>
            {
                Success = true,
                Message = "Estado del portal obtenido correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = response
            });
        })
        .WithName("GetCustomerPortalStatus")
        .Produces<ApiResponse<PortalStatusResponse>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
