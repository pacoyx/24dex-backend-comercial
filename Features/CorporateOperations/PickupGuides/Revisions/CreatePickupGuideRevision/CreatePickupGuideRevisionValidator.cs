using FluentValidation;

public class CreatePickupGuideRevisionValidator : AbstractValidator<CreatePickupGuideRevisionRequest>
{
    public CreatePickupGuideRevisionValidator()
    {
        RuleFor(x => x.EmployeeId).GreaterThan(0);
        RuleFor(x => x.Observation).MaximumLength(500);
        RuleFor(x => x.Details).NotEmpty()
            .WithMessage("La revisión debe incluir al menos un detalle corregido.");

        RuleForEach(x => x.Details).ChildRules(detail =>
        {
            detail.RuleFor(d => d.PickupGuideDetailId).GreaterThan(0);
            detail.RuleFor(d => d.NewQuantity).GreaterThanOrEqualTo(0);
            detail.RuleFor(d => d.Reason).NotEmpty().MaximumLength(500);
        });
    }
}
