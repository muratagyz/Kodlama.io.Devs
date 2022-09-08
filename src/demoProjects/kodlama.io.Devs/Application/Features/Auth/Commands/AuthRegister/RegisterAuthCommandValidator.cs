using FluentValidation;

namespace Application.Features.Auth.Commands.AuthRegister;

public class RegisterAuthCommandValidator : AbstractValidator<RegisterAuthCommand>
{
    public RegisterAuthCommandValidator()
    {
        RuleFor(r => r.Email).NotEmpty();
        RuleFor(r => r.Email).EmailAddress();
        RuleFor(r => r.Password).NotEmpty();
        RuleFor(r => r.FirstName).NotEmpty();
        RuleFor(r => r.LastName).NotEmpty();
    }
}