using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Domain.Entities;

public class Github : Entity
{
    public int UserId { get; set; }
    public string ProfileUrl { get; set; }
    public Github()
    {
        
    }
    public Github(int id, int userId, string profileUrl) : this()
    {
        Id = id;
        UserId = userId;
        ProfileUrl = profileUrl;
    }
}