using Application.Features.Github.Dtos;
using Application.Features.Github.Rules;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Github.Commands.DeleteGithub;

public class DeleteGithubCommand : IRequest<DeletedGithubDto>
{
    public int Id { get; set; }

    public class DeleteGithubCommandHandler : IRequestHandler<DeleteGithubCommand, DeletedGithubDto>
    {
        private readonly IGithubRepository _githubRepository;
        private readonly IMapper _mapper;
        private readonly GithubBusinessRules _businessRules;

        public DeleteGithubCommandHandler(IGithubRepository githubRepository, IMapper mapper, GithubBusinessRules businessRules)
        {
            _githubRepository = githubRepository;
            _mapper = mapper;
            _businessRules = businessRules;
        }

        public async Task<DeletedGithubDto> Handle(DeleteGithubCommand request, CancellationToken cancellationToken)
        {
            await _businessRules.GithubShouldExistWhenRequested(request.Id);
            Domain.Entities.Github? github = await _githubRepository.GetAsync(p => p.Id == request.Id);
            Domain.Entities.Github githubLanguage = await _githubRepository.DeleteAsync(github);
            DeletedGithubDto deletedGithubDto =
                _mapper.Map<DeletedGithubDto>(githubLanguage);
            return deletedGithubDto;
        }
    }
}