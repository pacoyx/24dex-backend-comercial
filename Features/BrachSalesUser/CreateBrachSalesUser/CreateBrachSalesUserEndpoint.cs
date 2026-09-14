using Microsoft.EntityFrameworkCore;

public static class CreateBrachSalesUserEndpoint
{
    public static void MapCreateBrachSalesUser(this IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (CreateBrachSalesUserDto createBrachSalesUserDto, RecepcionDbContext context) =>
        {
            if (createBrachSalesUserDto.Status == null)
            {
                return Results.BadRequest("Status es requerido");
            }

            var branchSales = await context.BranchSales.FirstOrDefaultAsync(bs => bs.Id == createBrachSalesUserDto.BranchSalesId);
            if (branchSales == null)
            {
                return Results.BadRequest("No se encontró la sucursal");
            }

            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == createBrachSalesUserDto.UserId);
            if (user == null)
            {
                return Results.BadRequest("No se encontró el usuario");
            }

            var brachSalesUser = new BrachSalesUser
            {
                BranchSalesId = createBrachSalesUserDto.BranchSalesId,
                UserId = createBrachSalesUserDto.UserId,
                Status = createBrachSalesUserDto.Status
            };

            await context.BrachSalesUsers.AddAsync(brachSalesUser);
            await context.SaveChangesAsync();

            var response = new ApiResponse<string>
            {
                Data = brachSalesUser.Id.ToString(),
                Message = "Sucursal de usuario creada",
                StatusCode = StatusCodes.Status201Created,
                Success = true
            };

            return Results.Ok(response);
        });
    }
}
