using FluentValidation;

public class CreateItemPhotoValidator : AbstractValidator<CreateItemPhotoRequest>
{
    public CreateItemPhotoValidator()
    {
        RuleFor(x => x.FileName).NotEmpty().MaximumLength(260);
        RuleFor(x => x.OriginalFileName).MaximumLength(260);
        RuleFor(x => x.BlobUrl).NotEmpty().MaximumLength(1000);
    }
}
