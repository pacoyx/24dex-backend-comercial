public static class PickupGuideAttachmentsEndpoints
{
    internal static PickupGuideAttachmentResponse ToResponse(PickupGuideAttachment attachment) => new(
        attachment.Id, attachment.PickupGuideId, attachment.AttachmentType, attachment.OriginalFileName,
        attachment.ContentType, attachment.FileSize, attachment.IsCurrent, attachment.CreatedAt
    );
}
