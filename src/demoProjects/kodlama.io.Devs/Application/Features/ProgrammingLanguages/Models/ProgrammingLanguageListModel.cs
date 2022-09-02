using Application.Features.ProgrammingLanguages.Dtos;

namespace Application.Features.ProgrammingLanguages.Models;

public class ProgrammingLanguageListModel
{
    public IList<ProgrammingLanguageListDto> Items { get; set; }
}