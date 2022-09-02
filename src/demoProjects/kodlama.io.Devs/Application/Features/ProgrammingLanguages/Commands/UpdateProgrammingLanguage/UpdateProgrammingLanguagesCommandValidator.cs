using FluentValidation;

namespace Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage;

public class UpdateProgrammingLanguagesCommandValidator : AbstractValidator<UpdateProgrammingLanguagesCommand>
{
    public UpdateProgrammingLanguagesCommandValidator()
    {
        RuleFor(p => p.Id).NotEmpty();
        RuleFor(p => p.Name).NotEmpty();
    }
}