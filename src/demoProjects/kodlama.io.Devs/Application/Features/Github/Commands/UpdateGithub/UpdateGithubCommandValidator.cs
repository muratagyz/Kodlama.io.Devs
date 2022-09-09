using FluentValidation;

namespace Application.Features.Github.Commands.UpdateGithub;

public class UpdateGithubCommandValidator:AbstractValidator<UpdateGithubCommand>
{
    public UpdateGithubCommandValidator()
    {
        RuleFor(u => u.Id).NotEmpty();
        RuleFor(u => u.UserId).NotEmpty();
        RuleFor(u => u.ProfileUrl).NotEmpty();
    }
}