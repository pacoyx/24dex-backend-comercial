using FluentValidation;

public class RegisterPurchasePaymentValidator : AbstractValidator<RegisterPurchasePaymentRequestDto>
{
    public RegisterPurchasePaymentValidator()
    {
        RuleFor(x => x.PaymentMethodId).GreaterThan(0);
        RuleFor(x => x.Amount).GreaterThan(0);
        RuleFor(x => x.OperationNumber).MaximumLength(50);
        RuleFor(x => x.Observation).MaximumLength(500);
    }
}
