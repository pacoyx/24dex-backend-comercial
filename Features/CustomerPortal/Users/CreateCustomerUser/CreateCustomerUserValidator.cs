using FluentValidation;

public class CreateCustomerUserValidator : AbstractValidator<CreateCustomerUserRequest>
{
    public CreateCustomerUserValidator()
    {
        RuleFor(x => x.FullName).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(200);
        RuleFor(x => x.UserName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.CustomerRoleId).GreaterThan(0);
    }
}
