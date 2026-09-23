using FluentValidation;

public class SalesRegisterFilterValidator : AbstractValidator<SalesRegisterFilter>
{
    public SalesRegisterFilterValidator()
    {
        RuleFor(x => x.FromDate)
            .NotEqual(default(DateTime))
            .WithMessage("La fecha 'Desde' es requerida.");

        RuleFor(x => x.ToDate)
            .NotEqual(default(DateTime))
            .WithMessage("La fecha 'Hasta' es requerida.");

        RuleFor(x => x.ToDate)
            .GreaterThanOrEqualTo(x => x.FromDate)
            .WithMessage("La fecha 'Hasta' no puede ser anterior a la fecha 'Desde'.");

        RuleFor(x => x.BranchId)
            .GreaterThan(0)
            .When(x => x.BranchId.HasValue)
            .WithMessage("La sucursal indicada no es válida.");

        RuleFor(x => x.CustomerId)
            .GreaterThan(0)
            .When(x => x.CustomerId.HasValue)
            .WithMessage("El cliente indicado no es válido.");

        RuleFor(x => x.Status)
            .IsInEnum()
            .When(x => x.Status.HasValue)
            .WithMessage("El estado indicado no es válido.");
    }
}
