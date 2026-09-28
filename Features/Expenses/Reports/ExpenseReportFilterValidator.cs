using FluentValidation;

public class ExpenseReportFilterValidator : AbstractValidator<ExpenseReportFilter>
{
    public ExpenseReportFilterValidator()
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

        RuleFor(x => x.BranchSalesId)
            .GreaterThan(0)
            .When(x => x.BranchSalesId.HasValue)
            .WithMessage("La sucursal indicada no es válida.");

        RuleFor(x => x.ExpenseCategoryId)
            .GreaterThan(0)
            .When(x => x.ExpenseCategoryId.HasValue)
            .WithMessage("La categoría indicada no es válida.");

        RuleFor(x => x.SourceType)
            .IsInEnum()
            .When(x => x.SourceType.HasValue)
            .WithMessage("El tipo de origen indicado no es válido.");
    }
}
