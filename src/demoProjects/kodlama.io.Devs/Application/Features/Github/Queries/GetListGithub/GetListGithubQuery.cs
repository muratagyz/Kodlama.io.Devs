using Application.Features.Github.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using MediatR;

namespace Application.Features.Github.Queries.GetListGithub;

public class GetListGithubQuery : IRequest<GithubListModel>
{
    public PageRequest PageRequest { get; set; }

    public class GetListGithubQueryHandler : IRequestHandler<GetListGithubQuery, GithubListModel>
    {
        private readonly IGithubRepository _githubRepository;
        private readonly IMapper _mapper;

        public GetListGithubQueryHandler(IGithubRepository githubRepository, IMapper mapper)
        {
            _githubRepository = githubRepository;
            _mapper = mapper;
        }

        public async Task<GithubListModel> Handle(GetListGithubQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Domain.Entities.Github> githubs = await _githubRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);
            GithubListModel mappedGithubListModel =
                _mapper.Map<GithubListModel>(githubs);
            return mappedGithubListModel;
        }
    }
}