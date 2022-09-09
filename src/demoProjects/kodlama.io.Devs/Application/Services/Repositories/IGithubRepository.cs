using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IGithubRepository : IAsyncRepository<Github>, IRepository<Github>
{
    
}