using FluentValidation;

public class UpdateExpenseValidator : AbstractValidator<UpdateExpenseRequest>
{
    public UpdateExpenseValidator()
    {
        RuleFor(x => x.BranchSalesId).GreaterThan(0)
            .WithMessage("La sucursal es obligatoria.");

        RuleFor(x => x.ExpenseCategoryId).GreaterThan(0)
            .WithMessage("La categoría de gasto es obligatoria.");

        RuleFor(x => x.SourceType).IsInEnum()
            .WithMessage("El tipo de origen del gasto no es válido.");

        RuleFor(x => x.ExpenseDate).NotEqual(default(DateTime))
            .WithMessage("La fecha del gasto es obligatoria.");

        RuleFor(x => x.Amount).GreaterThan(0)
            .WithMessage("El monto debe ser mayor a 0.");

        RuleFor(x => x.Description).NotEmpty().MaximumLength(500)
            .WithMessage("La descripción es obligatoria.");

        RuleFor(x => x.DocumentNumber)
            .NotEmpty()
            .When(x => x.HasSupportingDocument)
            .WithMessage("Debe indicar el número de documento cuando el gasto tiene sustento.");

        RuleFor(x => x.DocumentNumber).MaximumLength(50);
        RuleFor(x => x.SupplierName).MaximumLength(150);
        RuleFor(x => x.Observation).MaximumLength(500);
    }
}
