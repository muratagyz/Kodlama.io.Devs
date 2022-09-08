using Application.Features.Auth.Commands.AuthRegister;
using AutoMapper;
using Core.Security.Entities;

namespace Application.Features.Auth.Profiles;

public class MappingProfiles:Profile
{
    public MappingProfiles()
    {
        CreateMap<User, RegisterAuthCommand>().ReverseMap();
    }
}