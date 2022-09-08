using Application.Features.Technology.Dtos;
using Application.Features.Technology.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Technology.Commands.DeleteTechnology;

public class DeleteTechnologyCommand : IRequest<DeletedTechnologyDto>
{
    public int Id { get; set; }

    public class DeleteTechnologyCommandHandler : IRequestHandler<DeleteTechnologyCommand, DeletedTechnologyDto>
    {
        private readonly ITechnologyRepository _technologyRepository;
        private readonly IMapper _mapper;
        private TechnologyBusinessRules _technologyBusinessRules;

        public DeleteTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyBusinessRules technologyBusinessRules)
        {
            _technologyRepository = technologyRepository;
            _mapper = mapper;
            _technologyBusinessRules = technologyBusinessRules;
        }

        public async Task<DeletedTechnologyDto> Handle(DeleteTechnologyCommand request, CancellationToken cancellationToken)
        {
            await _technologyBusinessRules.TechnologyShouldExistWhenRequested(request.Id);
            Domain.Entities.Technology? technology = await _technologyRepository.GetAsync(p => p.Id == request.Id);
            Domain.Entities.Technology technologyLanguage = await _technologyRepository.DeleteAsync(technology);
            DeletedTechnologyDto deletedTechnologyDto =
                _mapper.Map<DeletedTechnologyDto>(technologyLanguage);
            return deletedTechnologyDto;
        }
    }
}