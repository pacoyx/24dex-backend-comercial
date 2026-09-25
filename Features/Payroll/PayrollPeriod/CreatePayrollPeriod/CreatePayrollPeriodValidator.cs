using FluentValidation;

public class CreatePayrollPeriodValidator : AbstractValidator<CreatePayrollPeriodRequestDto>
{
    public CreatePayrollPeriodValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Frequency).IsInEnum();
        RuleFor(x => x.EndDate).GreaterThanOrEqualTo(x => x.StartDate)
            .WithMessage("La fecha de fin no puede ser anterior a la fecha de inicio.");
    }
}
