using Application.Features.Technology.Dtos;
using Application.Features.Technology.Rules;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Technology.Queries.GetByIdTechnology;

public class GetByIdTechnologyQuery : IRequest<TechnologyGetByIdDto>
{
    public int Id { get; set; }

    public class GetByIdTechnologyQueryHandler : IRequestHandler<GetByIdTechnologyQuery, TechnologyGetByIdDto>
    {
        private readonly ITechnologyRepository _technologyRepository;
        private readonly IMapper _mapper;
        private TechnologyBusinessRules _businessRules;

        public GetByIdTechnologyQueryHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyBusinessRules businessRules)
        {
            _technologyRepository = technologyRepository;
            _mapper = mapper;
            _businessRules = businessRules;
        }

        public async Task<TechnologyGetByIdDto> Handle(GetByIdTechnologyQuery request, CancellationToken cancellationToken)
        {
            await _businessRules.TechnologyShouldExistWhenRequested(request.Id);
            Domain.Entities.Technology? technology = await _technologyRepository.GetAsync(p => p.Id == request.Id, include: i => i.Include(i => i.ProgrammingLanguage));
            TechnologyGetByIdDto technologyGetByIdDto =
                _mapper.Map<TechnologyGetByIdDto>(technology);
            return technologyGetByIdDto;
        }
    }
}