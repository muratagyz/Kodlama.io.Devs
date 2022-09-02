using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage;

public class UpdateProgrammingLanguagesCommand : IRequest<UpdatedProgrammingLanguageDto>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public class UpdateProgrammingLanguagesCommandHandler : IRequestHandler<UpdateProgrammingLanguagesCommand, UpdatedProgrammingLanguageDto>
    {
        private readonly IProgrammingLanguageRepository _repository;
        private readonly IMapper _mapper;
        private readonly ProgrammingLanguageBusinessRules _businessRules;

        public UpdateProgrammingLanguagesCommandHandler(IProgrammingLanguageRepository repository, IMapper mapper, ProgrammingLanguageBusinessRules businessRules)
        {
            _repository = repository;
            _mapper = mapper;
            _businessRules = businessRules;
        }

        public async Task<UpdatedProgrammingLanguageDto> Handle(UpdateProgrammingLanguagesCommand request, CancellationToken cancellationToken)
        {
            await _businessRules.ProgrammingLanguageShouldExistWhenRequested(request.Id);
            await _businessRules.ProgrammingLanguageNameCanNotBeDuplicatedWhenUpdated(request.Name);
            ProgrammingLanguage? programmingLanguage = _mapper.Map<ProgrammingLanguage>(request);
            ProgrammingLanguage updatedProgrammingLanguage = await _repository.UpdateAsync(programmingLanguage);
            UpdatedProgrammingLanguageDto updatedProgrammingLanguageDto =
                _mapper.Map<UpdatedProgrammingLanguageDto>(updatedProgrammingLanguage);
            return updatedProgrammingLanguageDto;
        }
    }
}