using FluentValidation;

namespace Application.Features.Auth.Commands.AuthLogin;

public class LoginAuthCommandValidator : AbstractValidator<LoginAuthCommand>
{
    public LoginAuthCommandValidator()
    {
        RuleFor(l => l.Email).NotEmpty();
        RuleFor(l => l.Email).EmailAddress();
        RuleFor(l => l.Password).NotEmpty();
    }
}