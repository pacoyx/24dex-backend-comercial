public interface IGetPuchasesInvoiceService
{
    Task<GetPurchaseInvoiceByIdResponseDto> GetPurchaseInvoiceAsync(int id);
    Task<IEnumerable<GetPurchaseInvoiceResponseDto>> GetPurchasesInvoiceAsync();
}

