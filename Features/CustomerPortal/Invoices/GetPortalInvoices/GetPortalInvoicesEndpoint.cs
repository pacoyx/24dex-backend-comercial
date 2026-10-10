using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public static class GetPortalInvoicesEndpoint
{
    public static void MapGetPortalInvoices(this IEndpointRouteBuilder app)
    {
        app.MapGet("/invoices", async (
            ClaimsPrincipal principal,
            [FromQuery] DateTime? dateFrom,
            [FromQuery] DateTime? dateTo,
            [FromQuery] InvoiceStatus? status,
            RecepcionDbContext db) =>
        {
            var customerId = principal.GetCustomerId();
            if (customerId is null)
            {
                return Results.Unauthorized();
            }

            var query = db.Invoices
                .AsNoTracking()
                .Where(x => x.CustomerId == customerId.Value)
                .AsQueryable();

            if (dateFrom.HasValue)
            {
                query = query.Where(x => x.IssueDate.Date >= dateFrom.Value.Date);
            }

            if (dateTo.HasValue)
            {
                query = query.Where(x => x.IssueDate.Date <= dateTo.Value.Date);
            }

            if (status.HasValue)
            {
                query = query.Where(x => x.Status == status.Value);
            }

            var invoices = await query.OrderByDescending(x => x.IssueDate).ToListAsync();

            var response = invoices.Select(x => new PortalInvoiceSummaryResponse(
                x.Id, x.Serie, x.Numero, x.CurrencyId, x.IssueDate, x.DueDate, x.Total, x.AmountPaid, x.Balance, x.Status
            )).ToList();

            return Results.Ok(new ApiResponse<List<PortalInvoiceSummaryResponse>>
            {
                Success = true,
                Message = "Listado de facturas de la empresa.",
                StatusCode = StatusCodes.Status200OK,
                Data = response
            });
        })
        .WithName("GetPortalInvoices")
        .Produces<ApiResponse<List<PortalInvoiceSummaryResponse>>>(StatusCodes.Status200OK)
        .RequireCustomerPermission(CustomerPermissionCodes.InvoicesView);
    }
}
