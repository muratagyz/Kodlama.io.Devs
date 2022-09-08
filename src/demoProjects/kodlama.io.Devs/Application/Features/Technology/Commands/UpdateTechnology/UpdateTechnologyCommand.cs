using Application.Features.Technology.Dtos;
using Application.Features.Technology.Rules;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Technology.Commands.UpdateTechnology;

public class UpdateTechnologyCommand : IRequest<UpdatedTechnologyDto>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ProgrammingLanguageId { get; set; }

    public class UpdateTechnologyCommandHandler : IRequestHandler<UpdateTechnologyCommand, UpdatedTechnologyDto>
    {
        private readonly ITechnologyRepository _technologyRepository;
        private readonly IMapper _mapper;
        private TechnologyBusinessRules _technologyBusinessRules;

        public UpdateTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyBusinessRules technologyBusinessRules)
        {
            _technologyRepository = technologyRepository;
            _mapper = mapper;
            _technologyBusinessRules = technologyBusinessRules;
        }

        public async Task<UpdatedTechnologyDto> Handle(UpdateTechnologyCommand request, CancellationToken cancellationToken)
        {
            await _technologyBusinessRules.TechnologyShouldExistWhenRequested(request.Id);
            await _technologyBusinessRules.TechnologyNameCanNotBeDuplicatedWhenUpdated(request.Name);
            await _technologyBusinessRules.ProgrammingLanguageShouldExistWhenRequested(request.ProgrammingLanguageId);
            Domain.Entities.Technology? technology = _mapper.Map<Domain.Entities.Technology>(request);
            Domain.Entities.Technology updatedTechnology = await _technologyRepository.UpdateAsync(technology);
            UpdatedTechnologyDto updatedTechnologyDtoDto =
                _mapper.Map<UpdatedTechnologyDto>(updatedTechnology);
            return updatedTechnologyDtoDto;
        }
    }
}