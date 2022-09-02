using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage;

public class DeleteProgrammingLanguagesCommand : IRequest<DeletedProgrammingLanguageDto>
{
    public int Id { get; set; }
    public class DeleteProgrammingLanguagesCommandHandler : IRequestHandler<DeleteProgrammingLanguagesCommand, DeletedProgrammingLanguageDto>
    {
        private readonly IProgrammingLanguageRepository _repository;
        private readonly IMapper _mapper;
        private readonly ProgrammingLanguageBusinessRules _businessRules;

        public DeleteProgrammingLanguagesCommandHandler(IProgrammingLanguageRepository repository, IMapper mapper, ProgrammingLanguageBusinessRules businessRules)
        {
            _repository = repository;
            _mapper = mapper;
            _businessRules = businessRules;
        }

        public async Task<DeletedProgrammingLanguageDto> Handle(DeleteProgrammingLanguagesCommand request, CancellationToken cancellationToken)
        {
            await _businessRules.ProgrammingLanguageShouldExistWhenRequested(request.Id);
            ProgrammingLanguage? programmingLanguage = await _repository.GetAsync(p => p.Id == request.Id);
            ProgrammingLanguage deletedProgrammingLanguage = await _repository.DeleteAsync(programmingLanguage);
            DeletedProgrammingLanguageDto deletedProgrammingLanguageDto =
                _mapper.Map<DeletedProgrammingLanguageDto>(deletedProgrammingLanguage);
            return deletedProgrammingLanguageDto;
        }
    }
}