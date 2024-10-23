using Microsoft.EntityFrameworkCore;
public static class DeleteCustomerEndpoint
{
    public static void MapDeleteCustomer(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/customer/{id}", async (RecepcionDbContext context, int id) =>
        {
            var customer = await context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (customer == null)
            {
                return Results.NotFound();
            }
            context.Customers.Remove(customer);
            await context.SaveChangesAsync();
            return Results.Ok();
        });
    }
}