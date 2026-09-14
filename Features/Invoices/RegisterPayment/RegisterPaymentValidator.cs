using FluentValidation;

public class RegisterPaymentValidator : AbstractValidator<RegisterPaymentRequest>
{
    public RegisterPaymentValidator()
    {
        RuleFor(x => x.PaymentMethodId).GreaterThan(0);
        RuleFor(x => x.Amount).GreaterThan(0);
        RuleFor(x => x.OperationNumber).MaximumLength(50);
        RuleFor(x => x.Observation).MaximumLength(500);
    }
}
