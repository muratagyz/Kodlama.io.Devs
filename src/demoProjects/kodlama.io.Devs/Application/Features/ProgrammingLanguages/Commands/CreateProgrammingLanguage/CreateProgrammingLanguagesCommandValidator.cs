using FluentValidation;

namespace Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;

public class CreateProgrammingLanguagesCommandValidator : AbstractValidator<CreateProgrammingLanguagesCommand>
{
    public CreateProgrammingLanguagesCommandValidator()
    {
        RuleFor(p => p.Name).NotEmpty();
        RuleFor(p => p.Name).MinimumLength(2);
    }
}