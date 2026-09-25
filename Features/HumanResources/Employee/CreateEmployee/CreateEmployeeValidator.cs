using FluentValidation;

public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeRequestDto>
{
    public CreateEmployeeValidator()
    {
        RuleFor(x => x.Code).NotEmpty().MaximumLength(20);
        RuleFor(x => x.DocumentNumber).NotEmpty().MaximumLength(20);
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.PhoneNumber).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Address).MaximumLength(200);
        RuleFor(x => x.AreaId).GreaterThan(0);
        RuleFor(x => x.PositionId).GreaterThan(0);
        RuleFor(x => x.ContractType).IsInEnum();
        RuleFor(x => x.BaseSalary).GreaterThanOrEqualTo(0);
        RuleFor(x => x.HireDate).GreaterThanOrEqualTo(x => x.BirthDate)
            .WithMessage("La fecha de contratación no puede ser anterior a la fecha de nacimiento.");

        RuleForEach(x => x.EmergencyContacts).ChildRules(contact =>
        {
            contact.RuleFor(c => c.Name).NotEmpty().MaximumLength(150);
            contact.RuleFor(c => c.Relationship).NotEmpty().MaximumLength(50);
            contact.RuleFor(c => c.PhoneNumber).NotEmpty().MaximumLength(20);
        });

        RuleForEach(x => x.BankAccounts).ChildRules(account =>
        {
            account.RuleFor(a => a.BankName).NotEmpty().MaximumLength(100);
            account.RuleFor(a => a.AccountNumber).NotEmpty().MaximumLength(30);
        });
    }
}
