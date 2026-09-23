using FluentValidation;

public class UpsertSneakerDetailValidator : AbstractValidator<UpsertSneakerDetailRequest>
{
    public UpsertSneakerDetailValidator()
    {
        RuleFor(x => x.BrandId).GreaterThan(0).When(x => x.BrandId.HasValue);
        RuleFor(x => x.PrimaryColor).MaximumLength(50);
        RuleFor(x => x.SecondaryColor).MaximumLength(50);
        RuleFor(x => x.Observations).MaximumLength(500);
    }
}
