using CustomerApp.Application.UserFeatures.Commands.LoginWithCredentials;
using FluentValidation;

namespace CustomerApp.Application.UserFeatures.Validators;

public class LoginWithCredentialsCommandValidator : AbstractValidator<LoginWithCredentialsCommand>
{
    public LoginWithCredentialsCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}
