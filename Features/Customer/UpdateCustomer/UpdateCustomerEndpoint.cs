public static class UpdateCustomerEndpoint
{
    public static void MapUpdateCustomer(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPut("/{id}", async (RecepcionDbContext context, int id, Customer request) =>
        {
            if (request == null || request.Id != Convert.ToInt32(id))
            {
                return Results.BadRequest();
            }
            
            var existing = await context.Customers.FindAsync(Convert.ToInt32(id));
            if (existing == null)
            {
                return Results.NotFound();
            }

            existing.FirtsName = request.FirtsName;
            existing.LastName = request.LastName;
            existing.Address = request.Address;
            existing.Phone = request.Phone;
            existing.Email = request.Email;
            existing.DocPersonal = request.DocPersonal;
            existing.Status = request.Status;

            context.Customers.Update(existing);
            await context.SaveChangesAsync();
            return Results.Ok(existing);

        });
    }
}