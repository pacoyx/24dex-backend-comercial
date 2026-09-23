using FluentValidation;

public class CreateItemInspectionValidator : AbstractValidator<CreateItemInspectionRequest>
{
    public CreateItemInspectionValidator()
    {
        RuleFor(x => x.Notes).MaximumLength(500);
    }
}
