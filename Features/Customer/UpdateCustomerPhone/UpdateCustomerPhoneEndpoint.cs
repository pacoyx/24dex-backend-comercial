public static class UpdateCustomerPhoneEndpoint{
    public static void MapUpdateCustomerPhone(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPut("/phone/{id}", async (RecepcionDbContext context, int id, UpdateCustomerPhoneRequestDto request) =>
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

            existing.Phone = request.Phone;

            context.Customers.Update(existing);
            await context.SaveChangesAsync();

            var response = new ApiResponse<string>
            {
                Success = true,
                Data = "OK",
                StatusCode = StatusCodes.Status200OK,
                Message = "Customer phone updated successfully"
            };
            return Results.Ok(response);
        });
    }
}