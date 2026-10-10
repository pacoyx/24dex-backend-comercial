using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

public static class GetPortalInvoiceEndpoint
{
    public static void MapGetPortalInvoice(this IEndpointRouteBuilder app)
    {
        app.MapGet("/invoices/{id:long}", async (long id, ClaimsPrincipal principal, RecepcionDbContext db) =>
        {
            var customerId = principal.GetCustomerId();
            if (customerId is null)
            {
                return Results.Unauthorized();
            }

            var invoice = await db.Invoices
                .AsNoTracking()
                .Include(x => x.Details)
                .FirstOrDefaultAsync(x => x.Id == id && x.CustomerId == customerId.Value);

            if (invoice is null)
            {
                return Results.NotFound();
            }

            var response = new PortalInvoiceResponse(
                invoice.Id, invoice.Serie, invoice.Numero, invoice.CurrencyId, invoice.IssueDate, invoice.DueDate,
                invoice.SubTotal, invoice.Igv, invoice.Total, invoice.AmountPaid, invoice.Balance, invoice.Status, invoice.Notes,
                invoice.Details.Select(d => new PortalInvoiceDetailResponse(
                    d.Id, d.Description, d.Quantity, d.UnitPrice, d.Discount, d.Total
                )).ToList()
            );

            return Results.Ok(new ApiResponse<PortalInvoiceResponse>
            {
                Success = true,
                Message = "Factura obtenida correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = response
            });
        })
        .WithName("GetPortalInvoice")
        .Produces<ApiResponse<PortalInvoiceResponse>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .RequireCustomerPermission(CustomerPermissionCodes.InvoicesView);
    }
}
