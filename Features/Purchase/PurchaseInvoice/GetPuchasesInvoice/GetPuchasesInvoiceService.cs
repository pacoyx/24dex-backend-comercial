
using Microsoft.EntityFrameworkCore;

public class GetPuchasesInvoiceService : IGetPuchasesInvoiceService
{
    private readonly RecepcionDbContext _context;
    public IAppLogger<GetServicesAccessFastService> Logger { get; }

    public GetPuchasesInvoiceService(RecepcionDbContext context, IAppLogger<GetServicesAccessFastService> logger)
    {
        _context = context;
        Logger = logger;
    }

    async public Task<GetPurchaseInvoiceByIdResponseDto> GetPurchaseInvoiceAsync(int id)
    {
        var purchaseInvoice = await _context.PurchaseInvoices
            .Include(x => x.Supplier)
            .Include(x => x.PurchaseInvoiceDetails)
            .ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (purchaseInvoice == null)
        {
            return null; // or throw an exception, depending on your error handling strategy    
        }

        var response = new GetPurchaseInvoiceByIdResponseDto
        {
            Id = purchaseInvoice.Id,
            InvoiceType = purchaseInvoice.InvoiceType,
            InvoiceSerie = purchaseInvoice.InvoiceSerie,
            InvoiceNumber = purchaseInvoice.InvoiceNumber,
            InvoiceIssueDate = purchaseInvoice.InvoiceIssueDate,
            InvoiceExpirationDate = purchaseInvoice.InvoiceExpirationDate,
            TypePayment = purchaseInvoice.TypePayment,
            DaysCredit = purchaseInvoice.DaysCredit,
            PaymentMethod = purchaseInvoice.PaymentMethod,
            Total = purchaseInvoice.Total,
            Status = purchaseInvoice.Status,
            PurchaseDate = purchaseInvoice.PurchaseDate,
            SupplierId = purchaseInvoice.SupplierId,
            SupplierName = purchaseInvoice.Supplier?.Name ?? string.Empty, // Safely accessing Supplier.Name
            PurchaseInvoiceDetails = (purchaseInvoice.PurchaseInvoiceDetails ?? new List<PurchaseInvoiceDetails>()).Select(detail => new PurchaseInvoiceDetailsDto
            {
                ProductId = detail.ProductId,
                Quantity = detail.Quantity,
                Price = detail.Price,
                Total = detail.Total,
                ProductName = detail.Product != null ? detail.Product.Name : string.Empty // Safely accessing Product.Name
            }).ToList()
        };

        return response;
    }

    async public Task<IEnumerable<GetPurchaseInvoiceResponseDto>> GetPurchasesInvoiceAsync()
    {
        var purchaseInvoices = await _context.PurchaseInvoices
            .Include(x => x.Supplier)
            .Select(x => new GetPurchaseInvoiceResponseDto
            {
                Id = x.Id,
                InvoiceType = x.InvoiceType,
                InvoiceSerie = x.InvoiceSerie,
                InvoiceNumber = x.InvoiceNumber,
                InvoiceIssueDate = x.InvoiceIssueDate,
                InvoiceExpirationDate = x.InvoiceExpirationDate,
                TypePayment = x.TypePayment,
                DaysCredit = x.DaysCredit,
                PaymentMethod = x.PaymentMethod,
                Total = x.Total,
                Status = x.Status,
                PurchaseDate = x.PurchaseDate,
                SupplierId = x.SupplierId,
                SupplierName = x.Supplier != null ? x.Supplier.Name : string.Empty // Safely accessing Supplier.Name
            }).ToListAsync();

        return purchaseInvoices;
    }
}