using Microsoft.EntityFrameworkCore;
public static class DeleteCustomerEndpoint
{
    public static void MapDeleteCustomer(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id}", async (RecepcionDbContext context, int id) =>
        {
            var customer = await context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (customer == null)
            {
                return Results.NotFound();
            }
            customer.Status = "I";

            await context.SaveChangesAsync();
            var response = new ApiResponse<string>
            {
                Success = true,
                Data = "OK",
                StatusCode = StatusCodes.Status200OK,
                Message = "Customer deleted successfully"
            };
            return Results.Ok(response);
        });
    }
}