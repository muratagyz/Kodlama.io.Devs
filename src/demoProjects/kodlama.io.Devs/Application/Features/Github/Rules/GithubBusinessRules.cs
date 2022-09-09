using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;

namespace Application.Features.Github.Rules;

public class GithubBusinessRules
{
    private readonly IGithubRepository _githubRepository;
    private readonly IUserRepository _userRepository;

    public GithubBusinessRules(IGithubRepository githubRepository, IUserRepository userRepository)
    {
        _githubRepository = githubRepository;
        _userRepository = userRepository;
    }

    public async Task GithubShouldExistWhenRequested(int id)
    {
        Domain.Entities.Github? github = await _githubRepository.GetAsync(g => g.Id == id);
        if (github == null) throw new BusinessException("Requested technology does not exist.");
    }

    public async Task UserShouldExistWhenRequested(int userUd)
    {
        User? user = await _userRepository.GetAsync(u => u.Id == userUd);
        if (user == null) throw new BusinessException("Requested user does not exist.");
    }

    public async Task GithubNameCanNotBeDuplicatedWhenInserted(int userId)
    {
        IPaginate<Domain.Entities.Github> result = await _githubRepository.GetListAsync(g => g.UserId == userId);
        if (result.Items.Any()) throw new BusinessException("Github user id exists.");
    }
}