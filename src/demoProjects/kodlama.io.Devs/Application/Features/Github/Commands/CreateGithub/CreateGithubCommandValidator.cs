using FluentValidation;

namespace Application.Features.Github.Commands.CreateGithub;

public class CreateGithubCommandValidator : AbstractValidator<CreateGithubCommand>
{
    public CreateGithubCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.ProfileUrl).NotEmpty();
    }
}