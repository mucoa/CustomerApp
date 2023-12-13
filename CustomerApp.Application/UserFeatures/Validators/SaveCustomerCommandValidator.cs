using CustomerApp.Application.CustomerFeatures.Commands.SaveCustomer;
using FluentValidation;

namespace CustomerApp.Application.UserFeatures.Validators;

public class SaveCustomerCommandValidator : AbstractValidator<SaveCustomerCommand>
{
    public SaveCustomerCommandValidator()
    {
        RuleFor(x => x.Identity)
            .NotEmpty()
            .Length(11);

        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.PhoneNumber)
            .NotEmpty();

        RuleFor(x => x.EmailAddress)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Address)
            .NotEmpty();

        RuleFor(x => x.BirthDate)
            .NotEmpty()
            .GreaterThan(new DateTime(1923, 01, 01)) 
            .LessThan(new DateTime(2010, 01, 01)); 
    }
}
