public interface IRegisterPurchasePaymentService
{
    Task<RegisterPurchasePaymentResult> RegisterPaymentAsync(int purchaseInvoiceId, RegisterPurchasePaymentRequestDto request);
}
