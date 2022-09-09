using Application.Features.Github.Dtos;
using Application.Features.Github.Rules;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Github.Commands.UpdateGithub;

public class UpdateGithubCommand : IRequest<UpdatedGithubDto>
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string ProfileUrl { get; set; }

    public class UpdateGithubCommandHandler : IRequestHandler<UpdateGithubCommand, UpdatedGithubDto>
    {
        private readonly IGithubRepository _githubRepository;
        private readonly IMapper _mapper;
        private readonly GithubBusinessRules _businessRules;

        public UpdateGithubCommandHandler(IGithubRepository githubRepository, IMapper mapper, GithubBusinessRules businessRules)
        {
            _githubRepository = githubRepository;
            _mapper = mapper;
            _businessRules = businessRules;
        }

        public async Task<UpdatedGithubDto> Handle(UpdateGithubCommand request, CancellationToken cancellationToken)
        {
            await _businessRules.GithubShouldExistWhenRequested(request.Id);
            await _businessRules.UserShouldExistWhenRequested(request.UserId);
            Domain.Entities.Github? github = _mapper.Map<Domain.Entities.Github>(request);
            Domain.Entities.Github updatedGithub = await _githubRepository.UpdateAsync(github);
            UpdatedGithubDto updatedGithubDto =
                _mapper.Map<UpdatedGithubDto>(updatedGithub);
            return updatedGithubDto;
        }
    }
}