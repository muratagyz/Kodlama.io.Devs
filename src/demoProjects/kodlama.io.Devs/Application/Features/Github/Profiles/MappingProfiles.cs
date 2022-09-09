using Application.Features.Github.Commands.CreateGithub;
using Application.Features.Github.Commands.UpdateGithub;
using Application.Features.Github.Dtos;
using Application.Features.Github.Models;
using AutoMapper;
using Core.Persistence.Paging;

namespace Application.Features.Github.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Domain.Entities.Github, GithubListModel>().ReverseMap();
        CreateMap<IPaginate<Domain.Entities.Github>, GithubListModel>().ReverseMap();
        CreateMap<Domain.Entities.Github, GithubGetByIdDto>().ReverseMap();
        CreateMap<Domain.Entities.Github, GithubListDto>().ReverseMap();
        CreateMap<Domain.Entities.Github, CreateGithubCommand>().ReverseMap();
        CreateMap<Domain.Entities.Github, CreatedGithubDto>().ReverseMap();
        CreateMap<Domain.Entities.Github, UpdateGithubCommand>().ReverseMap();
        CreateMap<Domain.Entities.Github, UpdatedGithubDto>().ReverseMap();
        CreateMap<Domain.Entities.Github, DeletedGithubDto>().ReverseMap();
    }
}