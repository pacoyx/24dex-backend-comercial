using Microsoft.EntityFrameworkCore;

public static class GetCustomerEndpoint
{
    public static void MapGetCustomer(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", async (RecepcionDbContext context, int id) =>
        {
            var customer = await context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (customer == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(customer);
        });
    }
}