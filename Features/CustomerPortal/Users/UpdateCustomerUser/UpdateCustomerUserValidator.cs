using FluentValidation;

public class UpdateCustomerUserValidator : AbstractValidator<UpdateCustomerUserRequest>
{
    public UpdateCustomerUserValidator()
    {
        RuleFor(x => x.FullName).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(200);
        RuleFor(x => x.CustomerRoleId).GreaterThan(0);
    }
}
