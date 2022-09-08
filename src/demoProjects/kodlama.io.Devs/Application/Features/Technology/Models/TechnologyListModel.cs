using Application.Features.Technology.Dtos;

namespace Application.Features.Technology.Models;

public class TechnologyListModel
{
    public IList<TechnologyListDto> Items { get; set; }
}