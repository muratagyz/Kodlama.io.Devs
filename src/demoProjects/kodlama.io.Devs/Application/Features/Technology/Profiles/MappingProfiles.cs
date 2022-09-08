using Application.Features.Technology.Commands.CreateTechnology;
using Application.Features.Technology.Commands.DeleteTechnology;
using Application.Features.Technology.Commands.UpdateTechnology;
using Application.Features.Technology.Dtos;
using Application.Features.Technology.Models;
using AutoMapper;
using Core.Persistence.Paging;

namespace Application.Features.Technology.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Domain.Entities.Technology, TechnologyListDto>().ForMember(t => t.ProgrammingLanguageName,
            opt => opt.MapFrom(c => c.ProgrammingLanguage.Name)).ReverseMap();
        CreateMap<Domain.Entities.Technology, TechnologyListModel>().ReverseMap();
        CreateMap<IPaginate<Domain.Entities.Technology>, TechnologyListModel>().ReverseMap();
        CreateMap<Domain.Entities.Technology, TechnologyGetByIdDto>().ForMember(t => t.ProgrammingLanguageName,
            opt => opt.MapFrom(c => c.ProgrammingLanguage.Name)).ReverseMap();
        CreateMap<Domain.Entities.Technology, CreatedTechnologyDto>().ReverseMap();
        CreateMap<Domain.Entities.Technology, CreateTechnologyCommand>().ReverseMap();
        CreateMap<Domain.Entities.Technology, DeletedTechnologyDto>().ReverseMap();
        CreateMap<Domain.Entities.Technology, DeleteTechnologyCommand>().ReverseMap();
        CreateMap<Domain.Entities.Technology, UpdatedTechnologyDto>().ReverseMap();
        CreateMap<Domain.Entities.Technology, UpdateTechnologyCommand>().ReverseMap();
    }
}