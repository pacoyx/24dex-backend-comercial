public interface IGetPurchasePaymentsService
{
    Task<List<RegisterPurchasePaymentResponseDto>?> GetPaymentsAsync(int purchaseInvoiceId);
}
