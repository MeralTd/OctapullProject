using Application.Features.Meetings.Commands;
using Application.Features.Meetings.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Meetings.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Meeting, MeetingDto>().ReverseMap();
        CreateMap<Meeting, CreateMeeting>().ReverseMap();
        CreateMap<Meeting, UpdateMeeting>().ReverseMap();


    }
}