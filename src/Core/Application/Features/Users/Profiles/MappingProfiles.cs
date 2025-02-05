using Application.Features.Authorizations.Commands;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Users.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, RegisterUser>().ReverseMap();
    }
}