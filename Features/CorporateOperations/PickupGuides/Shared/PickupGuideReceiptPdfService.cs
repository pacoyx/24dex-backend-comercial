using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;

public class PickupGuideReceiptPdfService : IPickupGuideReceiptPdfService
{
    private readonly RecepcionDbContext _db;
    private readonly IFileStorageService _storage;
    private readonly IImageCompressor _imageCompressor;

    public PickupGuideReceiptPdfService(RecepcionDbContext db, IFileStorageService storage, IImageCompressor imageCompressor)
    {
        _db = db;
        _storage = storage;
        _imageCompressor = imageCompressor;
    }

    public async Task<byte[]> GenerateAsync(long pickupGuideId, CancellationToken cancellationToken = default)
    {
        var guide = await _db.PickupGuides
            .AsNoTracking()
            .Include(x => x.Customer).ThenInclude(c => c.BillingProfile)
            .Include(x => x.Employee)
            .Include(x => x.Vehicle)
            .Include(x => x.Details).ThenInclude(d => d.ServiceItem)
            .FirstOrDefaultAsync(x => x.Id == pickupGuideId, cancellationToken);

        if (guide is null)
        {
            throw new InvalidOperationException($"Guía de recojo {pickupGuideId} no encontrada.");
        }

        var attachments = await _db.PickupGuideAttachments
            .AsNoTracking()
            .Where(x => x.PickupGuideId == pickupGuideId && x.IsCurrent)
            .ToListAsync(cancellationToken);

        byte[]? signatureImage = null;
        var signatureAttachment = attachments.FirstOrDefault(x => x.AttachmentType == PickupGuideAttachmentType.Signature);
        if (signatureAttachment is not null)
        {
            signatureImage = await ReadAllBytesAsync(signatureAttachment.FilePath, cancellationToken);
        }

        var photoImages = new List<byte[]>();
        foreach (var photoAttachment in attachments.Where(x => x.AttachmentType == PickupGuideAttachmentType.Photo))
        {
            var bytes = await ReadAllBytesAsync(photoAttachment.FilePath, cancellationToken);
            photoImages.Add(_imageCompressor.CompressToJpeg(bytes));
        }

        var document = new PickupGuideReceiptPdfDocument(guide, signatureImage, photoImages);
        return document.GeneratePdf();
    }

    private async Task<byte[]> ReadAllBytesAsync(string filePath, CancellationToken cancellationToken)
    {
        await using var stream = await _storage.OpenReadAsync(filePath, cancellationToken);
        using var memoryStream = new MemoryStream();
        await stream.CopyToAsync(memoryStream, cancellationToken);
        return memoryStream.ToArray();
    }
}
