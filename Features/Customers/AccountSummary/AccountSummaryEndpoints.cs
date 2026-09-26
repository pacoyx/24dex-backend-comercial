using Microsoft.EntityFrameworkCore;

public static class AccountSummaryEndpoints
{
    public static void MapAccountSummary(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:int}/account-summary", async (int id, RecepcionDbContext db) =>
        {
            var customer = await db.Customers
                .AsNoTracking()
                .Include(x => x.BillingProfile)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (customer is null)
            {
                return Results.NotFound("Cliente no encontrado.");
            }

            var invoicesQuery = db.Invoices
                .AsNoTracking()
                .Where(x => x.CustomerId == id && x.Status != InvoiceStatus.Cancelled);

            var currentBalance = await invoicesQuery.SumAsync(x => (decimal?)x.Balance) ?? 0m;
            var pendingInvoices = await invoicesQuery.CountAsync(x => x.Balance > 0);

            var creditLimit = customer.BillingProfile?.CreditLimit ?? 0m;
            var businessName = customer.BillingProfile?.BusinessName
                ?? $"{customer.FirtsName} {customer.LastName}".Trim();

            var response = new CustomerAccountSummaryResponseDto(
                customer.Id,
                businessName,
                creditLimit,
                currentBalance,
                creditLimit - currentBalance,
                pendingInvoices);

            return Results.Ok(new ApiResponse<CustomerAccountSummaryResponseDto>
            {
                Success = true,
                Message = "Resumen de cuenta obtenido correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = response
            });
        })
        .WithName("GetCustomerAccountSummary")
        .Produces<ApiResponse<CustomerAccountSummaryResponseDto>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .RequireAuthorization();
    }
}
