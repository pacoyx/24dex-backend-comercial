using Microsoft.EntityFrameworkCore;

public static class GetBrachSalesUserEndpoint
{
    public static void MapGetBrachSalesUser(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", async (int id, RecepcionDbContext context) =>
        {
            var brachSalesUser = await context.BrachSalesUsers.AsNoTracking()
            .Where(bsu => bsu.Id == id)
            .Join(context.Users.AsNoTracking(),
                bsu => bsu.UserId,
                u => u.Id,
                (bsu, u) => new BrachSalesUserComboResponse(
                    bsu.Id,
                    bsu.BranchSalesId,
                    bsu.UserId,
                    u.Name,
                    bsu.Status
                ))
            .FirstOrDefaultAsync();

            if (brachSalesUser == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(brachSalesUser);
        });
    }
}
