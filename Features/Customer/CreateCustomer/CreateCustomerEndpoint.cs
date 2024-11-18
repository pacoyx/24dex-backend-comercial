public static class CreateCustomerEndpoint
{
    public static void MapCreateCustomer(this IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (CreateCustomerDto customerDto, RecepcionDbContext context) =>
        {
            if (customerDto == null)
            {
                return Results.BadRequest();
            }

            var customer = new Customer()
            {
                FirtsName = customerDto.FirstName,
                LastName = customerDto.LastName,
                Address = customerDto.Address,
                Phone = customerDto.Phone,
                Email = customerDto.Email,
                DocPersonal = customerDto.DocPersonal,
                Status = customerDto.Status
            };

            await context.Customers.AddAsync(customer);
            await context.SaveChangesAsync();
            return Results.Ok(customer);            
        });


    }
}