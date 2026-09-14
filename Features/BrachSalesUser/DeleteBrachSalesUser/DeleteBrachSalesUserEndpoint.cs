using Microsoft.EntityFrameworkCore;

public static class DeleteBrachSalesUserEndpoint
{
    public static void MapDeleteBrachSalesUser(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id}", async (RecepcionDbContext context, int id) =>
        {
            var brachSalesUser = await context.BrachSalesUsers
            .FirstOrDefaultAsync(bsu => bsu.Id == id);

            if (brachSalesUser == null)
            {
                return Results.NotFound();
            }

            context.BrachSalesUsers.Remove(brachSalesUser);
            await context.SaveChangesAsync();

            var response = new ApiResponse<string>
            {
                Data = "Sucursal de usuario eliminada",
                Message = "Sucursal de usuario eliminada",
                StatusCode = StatusCodes.Status200OK,
                Success = true
            };

            return Results.Ok(response);
        });
    }
}
