using FluentValidation;

public class AddPaymentToCollectionSheetValidator : AbstractValidator<AddPaymentToCollectionSheetRequest>
{
    public AddPaymentToCollectionSheetValidator()
    {
        RuleFor(x => x.InvoiceId).GreaterThan(0);
        RuleFor(x => x.PaymentMethodId).GreaterThan(0);
        RuleFor(x => x.Amount).GreaterThan(0);
        RuleFor(x => x.OperationNumber).MaximumLength(50);
        RuleFor(x => x.Observation).MaximumLength(500);
    }
}
