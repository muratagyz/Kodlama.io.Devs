using Application.Features.Technology.Dtos;
using FluentValidation;

namespace Application.Features.Technology.Commands.UpdateTechnology;

public class UpdateTechnologyCommandValidator : AbstractValidator<UpdatedTechnologyDto>
{
    public UpdateTechnologyCommandValidator()
    {
        RuleFor(t => t.Name).NotEmpty();
        RuleFor(t => t.Name).MinimumLength(2);
        RuleFor(t => t.ProgrammingLanguageId).NotEmpty();
    }
}