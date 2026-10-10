using FluentValidation;

public class EnablePortalValidator : AbstractValidator<EnablePortalRequest>
{
    public EnablePortalValidator()
    {
        RuleFor(x => x.FullName).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(200);
    }
}
