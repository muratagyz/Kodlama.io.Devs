using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Technology.Rules;

public class TechnologyBusinessRules
{
    private readonly ITechnologyRepository _technologyRepository;
    private readonly IProgrammingLanguageRepository _programmingLanguageRepository;

    public TechnologyBusinessRules(ITechnologyRepository technologyRepository, IProgrammingLanguageRepository programmingLanguageRepository)
    {
        _technologyRepository = technologyRepository;
        _programmingLanguageRepository = programmingLanguageRepository;
    }

    public async Task TechnologyNameCanNotBeDuplicatedWhenInserted(string name)
    {
        IPaginate<Domain.Entities.Technology> result = await _technologyRepository.GetListAsync(t => t.Name == name);
        if (result.Items.Any()) throw new BusinessException("Technology name exists.");
    }

    public async Task TechnologyShouldExistWhenRequested(int id)
    {
        Domain.Entities.Technology? language = await _technologyRepository.GetAsync(p => p.Id == id);
        if (language == null) throw new BusinessException("Requested technology does not exist.");
    }

    public async Task ProgrammingLanguageShouldExistWhenRequested(int id)
    {
        ProgrammingLanguage? language = await _programmingLanguageRepository.GetAsync(p => p.Id == id);
        if (language == null) throw new BusinessException("There is no programming language entered.");
    }

    public async Task TechnologyNameCanNotBeDuplicatedWhenUpdated(string name)
    {
        IPaginate<Domain.Entities.Technology> result = await _technologyRepository.GetListAsync(p => p.Name == name);
        if (result.Items.Any()) throw new BusinessException("Technology name exists.");
    }
}