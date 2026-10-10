public interface IPickupGuideReceiptPdfService
{
    Task<byte[]> GenerateAsync(long pickupGuideId, CancellationToken cancellationToken = default);
}
