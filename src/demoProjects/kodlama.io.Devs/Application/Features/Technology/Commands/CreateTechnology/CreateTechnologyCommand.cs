using Application.Features.Technology.Dtos;
using Application.Features.Technology.Rules;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Technology.Commands.CreateTechnology;

public class CreateTechnologyCommand : IRequest<CreatedTechnologyDto>
{
    public string Name { get; set; }
    public int ProgrammingLanguageId { get; set; }

    public class CreateTechnologyCommandHandler : IRequestHandler<CreateTechnologyCommand, CreatedTechnologyDto>
    {
        private readonly ITechnologyRepository _technologyRepository;
        private readonly IMapper _mapper;
        private TechnologyBusinessRules _technologyBusinessRules;

        public CreateTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyBusinessRules technologyBusinessRules)
        {
            _technologyRepository = technologyRepository;
            _mapper = mapper;
            _technologyBusinessRules = technologyBusinessRules;
        }

        public async Task<CreatedTechnologyDto> Handle(CreateTechnologyCommand request, CancellationToken cancellationToken)
        {
            await _technologyBusinessRules.TechnologyNameCanNotBeDuplicatedWhenInserted(request.Name);
            await _technologyBusinessRules.ProgrammingLanguageShouldExistWhenRequested(
                request.ProgrammingLanguageId);
            Domain.Entities.Technology mappedTechnology = _mapper.Map<Domain.Entities.Technology>(request);
            Domain.Entities.Technology createdTechnology = await _technologyRepository.AddAsync(mappedTechnology);
            CreatedTechnologyDto createdTechnologyDtoDto = _mapper.Map<CreatedTechnologyDto>(createdTechnology);
            return createdTechnologyDtoDto;
        }
    }
}