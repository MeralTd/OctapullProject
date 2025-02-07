using Application.Features.Authorizations.Commands;
using Application.Features.Users.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Users.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, RegisterUser>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();

    }
}