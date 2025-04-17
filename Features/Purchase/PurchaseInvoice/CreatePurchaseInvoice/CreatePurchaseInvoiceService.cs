
public class CreatePurchaseInvoiceService : ICreatePurchaseInvoiceService
{
    private readonly RecepcionDbContext _context;
    public IAppLogger<GetServicesAccessFastService> Logger { get; }

    public CreatePurchaseInvoiceService(RecepcionDbContext context, IAppLogger<GetServicesAccessFastService> logger)
    {
        _context = context;
        Logger = logger;
    }

    public Task<PurchaseInvoiceResponseDto> CreatePurchaseInvoiceAsync(PurchaseInvoiceRequestDto request)
    {
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
            PurchaseInvoiceDetails = request.PurchaseInvoiceDetails?
            .Select(detail => new PurchaseInvoiceDetails
            {
                ProductId = detail.ProductId,
                Quantity = detail.Quantity,
                Price = detail.Price,
                Total = detail.Total
            }).ToList()
        };

        _context.PurchaseInvoices.Add(purchaseInvoice);
        _context.SaveChanges();

        return Task.FromResult(new PurchaseInvoiceResponseDto
        {
            Id = purchaseInvoice.Id
        });
    }
}