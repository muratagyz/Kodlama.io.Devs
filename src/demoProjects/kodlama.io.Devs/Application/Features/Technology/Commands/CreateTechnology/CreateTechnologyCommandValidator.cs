using Application.Features.Technology.Dtos;
using FluentValidation;

namespace Application.Features.Technology.Commands.CreateTechnology;

public class CreateTechnologyCommandValidator : AbstractValidator<CreatedTechnologyDto>
{
    public CreateTechnologyCommandValidator()
    {
        RuleFor(t => t.Name).NotEmpty();
        RuleFor(t => t.Name).MinimumLength(2);
        RuleFor(t => t.ProgrammingLanguageId).NotEmpty();
    }
}