using FluentValidation;

public class CreateCollectionSheetValidator : AbstractValidator<CreateCollectionSheetRequest>
{
    public CreateCollectionSheetValidator()
    {
        RuleFor(x => x.BranchId).GreaterThan(0);
        RuleFor(x => x.Number).NotEmpty().MaximumLength(30);
        RuleFor(x => x.Remarks).MaximumLength(500);
    }
}
