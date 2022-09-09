using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class GithubRepository : EfRepositoryBase<Github, BaseDbContext>, IGithubRepository
{
    public GithubRepository(BaseDbContext context) : base(context)
    {
    }
}