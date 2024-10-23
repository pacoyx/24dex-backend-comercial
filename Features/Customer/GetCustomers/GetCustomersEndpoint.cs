using Microsoft.EntityFrameworkCore;

public static class GetCustomersEndpoint
{
    public static void MapGetCustomers(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (RecepcionDbContext context) =>
        {
            var customers = await context.Customers.AsNoTracking().ToListAsync();
        });
    }
}