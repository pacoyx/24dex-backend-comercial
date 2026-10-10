using FluentValidation;

public class ActivateAccountValidator : AbstractValidator<ActivateAccountRequest>
{
    public ActivateAccountValidator()
    {
        RuleFor(x => x.Token).NotEmpty();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
    }
}
