using Application.Features.Github.Dtos;
using Application.Features.Github.Rules;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Github.Commands.CreateGithub;

public class CreateGithubCommand : IRequest<CreatedGithubDto>
{
    public int UserId { get; set; }
    public string ProfileUrl { get; set; }

    public class CreateGithubCommandHandler : IRequestHandler<CreateGithubCommand, CreatedGithubDto>
    {
        private readonly IGithubRepository _githubRepository;
        private readonly IMapper _mapper;
        private readonly GithubBusinessRules _businessRules;

        public CreateGithubCommandHandler(IGithubRepository githubRepository, IMapper mapper, GithubBusinessRules businessRules)
        {
            _githubRepository = githubRepository;
            _mapper = mapper;
            _businessRules = businessRules;
        }

        public async Task<CreatedGithubDto> Handle(CreateGithubCommand request, CancellationToken cancellationToken)
        {
            await _businessRules.GithubNameCanNotBeDuplicatedWhenInserted(request.UserId);
            await _businessRules.UserShouldExistWhenRequested(request.UserId);
            Domain.Entities.Github mappedGithub = _mapper.Map<Domain.Entities.Github>(request);
            Domain.Entities.Github createdGithub = await _githubRepository.AddAsync(mappedGithub);
            CreatedGithubDto createdTGithubDto = _mapper.Map<CreatedGithubDto>(createdGithub);
            return createdTGithubDto;
        }
    }
}