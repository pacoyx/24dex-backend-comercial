using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

public static class CreateInvoiceEndpoint
{
    public static void MapCreateInvoice(this IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (
            CreateInvoiceRequest request,
            IValidator<CreateInvoiceRequest> validator,
            RecepcionDbContext db,
            IOptions<InvoiceSettings> invoiceSettings) =>
        {
            var validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return Results.BadRequest(validation.Errors.Select(e => e.ErrorMessage));
            }

            var customerExists = await db.Customers.AnyAsync(x => x.Id == request.CustomerId);
            if (!customerExists)
            {
                return Results.NotFound("Cliente no encontrado.");
            }

            var serieNumeroEnUso = await db.Invoices.AnyAsync(x =>
                x.Serie == request.Serie && x.Numero == request.Numero);
            if (serieNumeroEnUso)
            {
                return Results.BadRequest("Ya existe una factura registrada con esa serie y número.");
            }

            var details = request.Details.Select(d => new InvoiceDetail
            {
                ProductId = d.ProductId,
                Description = d.Description,
                Quantity = d.Quantity,
                UnitPrice = d.UnitPrice,
                Discount = d.Discount,
                Total = (d.Quantity * d.UnitPrice) - d.Discount
            }).ToList();

            var subTotal = details.Sum(d => d.Total);
            var igv = Math.Round(subTotal * invoiceSettings.Value.IgvRate, 2);
            var total = subTotal + igv;

            var invoice = new Invoice
            {
                CustomerId = request.CustomerId,
                BranchId = request.BranchId,
                DocumentTypeId = request.DocumentTypeId,
                Serie = request.Serie,
                Numero = request.Numero,
                IssueDate = request.IssueDate,
                DueDate = request.DueDate,
                Notes = request.Notes,
                SubTotal = subTotal,
                Igv = igv,
                Total = total,
                AmountPaid = 0,
                Balance = total,
                Status = InvoiceStatus.Issued,
                Details = details
            };

            await db.Invoices.AddAsync(invoice);
            await db.SaveChangesAsync();

            var response = new ApiResponse<CreateInvoiceResponse>
            {
                Success = true,
                Message = "Factura registrada correctamente.",
                StatusCode = StatusCodes.Status201Created,
                Data = new CreateInvoiceResponse(
                    invoice.Id,
                    invoice.Serie,
                    invoice.Numero,
                    invoice.SubTotal,
                    invoice.Igv,
                    invoice.Total,
                    invoice.Status)
            };

            return Results.Created($"/api/invoices/{invoice.Id}", response);
        })
        .RequireAuthorization();
    }
}
