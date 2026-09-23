using FluentValidation;

public class CreateInvoiceValidator : AbstractValidator<CreateInvoiceRequest>
{
    public CreateInvoiceValidator()
    {
        RuleFor(x => x.CustomerId).GreaterThan(0);
        RuleFor(x => x.BranchId).GreaterThan(0);
        RuleFor(x => x.DocumentTypeId).GreaterThan(0);
        RuleFor(x => x.Serie).NotEmpty().MaximumLength(10);
        RuleFor(x => x.Numero).NotEmpty().MaximumLength(20);
        RuleFor(x => x.CurrencyId).NotEmpty().MaximumLength(3);
        RuleFor(x => x.ExchangeRate).GreaterThan(0);
        RuleFor(x => x.DueDate).GreaterThanOrEqualTo(x => x.IssueDate)
            .WithMessage("La fecha de vencimiento no puede ser anterior a la fecha de emisión.");
        RuleFor(x => x.Notes).MaximumLength(500);

        RuleFor(x => x.Details).NotEmpty()
            .WithMessage("La factura debe tener al menos un detalle.");

        RuleForEach(x => x.Details).ChildRules(detail =>
        {
            detail.RuleFor(d => d.ProductId).GreaterThan(0);
            detail.RuleFor(d => d.Description).NotEmpty().MaximumLength(200);
            detail.RuleFor(d => d.Quantity).GreaterThan(0);
            detail.RuleFor(d => d.UnitPrice).GreaterThanOrEqualTo(0);
            detail.RuleFor(d => d.Discount).GreaterThanOrEqualTo(0);
        });
    }
}
