using Microsoft.EntityFrameworkCore;

public static class GetCustomerEndpoint
{
    public static void MapGetCustomer(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", async (RecepcionDbContext context, int id, IAppLogger<string> logger) =>
        {
            var customer = await context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (customer == null)
            {
                return Results.NotFound();
            }
            logger.LogInformacion("Customer {id} found", id);
            return Results.Ok(customer);
        });
    }
}