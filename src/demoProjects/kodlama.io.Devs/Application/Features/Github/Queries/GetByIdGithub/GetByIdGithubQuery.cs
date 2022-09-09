using Application.Features.Github.Dtos;
using Application.Features.Github.Rules;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Github.Queries.GetByIdGithub;

public class GetByIdGithubQuery : IRequest<GithubGetByIdDto>
{
    public int Id { get; set; }

    public class GetByIdGithubQueryHandler : IRequestHandler<GetByIdGithubQuery, GithubGetByIdDto>
    {
        private readonly IGithubRepository _githubRepository;
        private readonly IMapper _mapper;
        private readonly GithubBusinessRules _businessRules;

        public GetByIdGithubQueryHandler(IGithubRepository githubRepository, IMapper mapper, GithubBusinessRules businessRules)
        {
            _githubRepository = githubRepository;
            _mapper = mapper;
            _businessRules = businessRules;
        }

        public async Task<GithubGetByIdDto> Handle(GetByIdGithubQuery request, CancellationToken cancellationToken)
        {
            await _businessRules.GithubShouldExistWhenRequested(request.Id);
            Domain.Entities.Github github = await _githubRepository.GetAsync(g => g.Id == request.Id);
            GithubGetByIdDto mappedGithubDto =
                _mapper.Map<GithubGetByIdDto>(github);
            return mappedGithubDto;
        }
    }
}