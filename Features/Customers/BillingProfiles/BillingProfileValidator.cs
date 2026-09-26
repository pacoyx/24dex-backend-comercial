using FluentValidation;

public class CustomerBillingProfileValidator : AbstractValidator<CustomerBillingProfileRequestDto>
{
    public CustomerBillingProfileValidator()
    {
        RuleFor(x => x.TaxId)
            .NotEmpty()
            .Length(11)
            .Matches("^[0-9]+$")
            .WithMessage("El RUC debe tener 11 dígitos numéricos.");

        RuleFor(x => x.BusinessName).NotEmpty().MaximumLength(200);
        RuleFor(x => x.CommercialName).MaximumLength(200);
        RuleFor(x => x.FiscalAddress).NotEmpty().MaximumLength(300);
        RuleFor(x => x.BillingEmail).EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.BillingEmail));
        RuleFor(x => x.PaymentTermDays).GreaterThanOrEqualTo(0);
        RuleFor(x => x.CreditLimit).GreaterThanOrEqualTo(0);
    }
}
