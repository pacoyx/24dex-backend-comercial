
using Serilog;

public class CreatePurchaseInvoiceService : ICreatePurchaseInvoiceService
{
    private readonly RecepcionDbContext _context;
    public IAppLogger<GetServicesAccessFastService> Logger { get; }

    public CreatePurchaseInvoiceService(RecepcionDbContext context, IAppLogger<GetServicesAccessFastService> logger)
    {
        _context = context;
        Logger = logger;
    }

    public async Task<PurchaseInvoiceResponseDto> CreatePurchaseInvoiceAsync(PurchaseInvoiceRequestDto request)
    {

        // Convert the incoming date to UTC and then to the desired timezone
        // var invoiceIssueDateUnspecified = DateTime.SpecifyKind(request.InvoiceIssueDate, DateTimeKind.Unspecified);
        // var invoiceIssueDateUtc = TimeZoneInfo.ConvertTimeToUtc(invoiceIssueDateUnspecified, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));
        var invoiceIssueDateLocal = TimeZoneInfo.ConvertTimeFromUtc(request.InvoiceIssueDate.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));
        invoiceIssueDateLocal = invoiceIssueDateLocal.Date; // Set time to 00:00:00

        Logger.LogInformacion("fecha emision 3 " + invoiceIssueDateLocal);  
        
        
        
        DateTime fechaOperacion = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));
        var purchaseInvoice = new PurchaseInvoice
        {
            SupplierId = request.SupplierId,
            InvoiceType = request.InvoiceType,
            InvoiceSerie = request.InvoiceSerie,
            InvoiceNumber = request.InvoiceNumber,
            InvoiceIssueDate = request.InvoiceIssueDate,
            InvoiceExpirationDate = request.InvoiceExpirationDate,
            TypePayment = request.TypePayment,
            DaysCredit = request.DaysCredit,
            PaymentMethod = request.PaymentMethod,
            Total = request.Total,
            Status = "A", 
            PurchaseDate = fechaOperacion,
            CurrencyId = request.CurrencyId,
            ExchangeRate = request.ExchangeRate,
            Comments = request.Comments,
            Subtotal = request.Subtotal,
            Igv = request.Igv,
            PurchaseInvoiceDetails = request.PurchaseInvoiceDetails?
            .Select(detail => new PurchaseInvoiceDetails
            {
                ProductId = detail.ProductId,
                Quantity = detail.Quantity,
                Price = detail.Price,
                Total = detail.Total
            }).ToList()
        };

        await _context.PurchaseInvoices.AddAsync(purchaseInvoice);
        await _context.SaveChangesAsync();
        
        return new PurchaseInvoiceResponseDto
        {
            Id = purchaseInvoice.Id
        };
    }
}