using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Technology : Entity
{
    public string Name { get; set; }
    public int ProgrammingLanguageId { get; set; }
    public virtual ProgrammingLanguage? ProgrammingLanguage { get; set; }
    public Technology()
    {
        
    }
    public Technology(int id, string name, int programmingLanguageId) : this()
    {
        Id = id;
        Name = name;
        ProgrammingLanguageId = programmingLanguageId;
    }
}