using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.ProgrammingLanguages.Rules;

public class ProgrammingLanguageBusinessRules
{
    private readonly IProgrammingLanguageRepository _repository;

    public ProgrammingLanguageBusinessRules(IProgrammingLanguageRepository repository)
    {
        _repository = repository;
    }

    public async Task ProgrammingLanguageNameCanNotBeDuplicatedWhenInserted(string name)
    {
        IPaginate<ProgrammingLanguage> result = await _repository.GetListAsync(p => p.Name == name);
        if (result.Items.Any()) throw new BusinessException("ProgrammingLanguage name exists.");
    }

    public async Task ProgrammingLanguageNameCanNotBeDuplicatedWhenUpdated(string name)
    {
        IPaginate<ProgrammingLanguage> result = await _repository.GetListAsync(p => p.Name == name);
        if (result.Items.Any()) throw new BusinessException("ProgrammingLanguage name exists.");
    }

    public async Task ProgrammingLanguageShouldExistWhenRequested(int id)
    {
        ProgrammingLanguage? language = await _repository.GetAsync(p => p.Id == id);
        if (language == null) throw new BusinessException("Requested programmingLanguage does not exist.");
    }
}