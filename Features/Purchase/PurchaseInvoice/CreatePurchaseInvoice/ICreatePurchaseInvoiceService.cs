public interface ICreatePurchaseInvoiceService
{
    Task<PurchaseInvoiceResponseDto> CreatePurchaseInvoiceAsync(PurchaseInvoiceRequestDto request);
}