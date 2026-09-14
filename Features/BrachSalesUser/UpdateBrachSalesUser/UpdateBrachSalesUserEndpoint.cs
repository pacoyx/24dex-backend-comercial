using Microsoft.EntityFrameworkCore;

public static class UpdateBrachSalesUserEndpoint
{
    public static void MapUpdateBrachSalesUser(this IEndpointRouteBuilder app)
    {
        app.MapPut("/{id}", async (RecepcionDbContext context, int id, UpdateBrachSalesUserDto request) =>
        {
            var brachSalesUser = await context.BrachSalesUsers
            .FirstOrDefaultAsync(bsu => bsu.Id == id);

            if (brachSalesUser == null)
            {
                return Results.NotFound();
            }

            brachSalesUser.BranchSalesId = request.BranchSalesId;
            brachSalesUser.UserId = request.UserId;
            brachSalesUser.Status = request.Status;

            await context.SaveChangesAsync();

            var response = new ApiResponse<string>
            {
                Data = "Sucursal de usuario actualizada",
                Message = "Sucursal de usuario actualizada",
                StatusCode = StatusCodes.Status200OK,
                Success = true
            };

            return Results.Ok(response);
        });
    }
}
