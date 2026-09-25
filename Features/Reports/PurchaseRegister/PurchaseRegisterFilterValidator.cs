using FluentValidation;

public class PurchaseRegisterFilterValidator : AbstractValidator<PurchaseRegisterFilter>
{
    public PurchaseRegisterFilterValidator()
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

        RuleFor(x => x.SupplierId)
            .GreaterThan(0)
            .When(x => x.SupplierId.HasValue)
            .WithMessage("El proveedor indicado no es válido.");

        RuleFor(x => x.Status)
            .IsInEnum()
            .When(x => x.Status.HasValue)
            .WithMessage("El estado indicado no es válido.");
    }
}
