using Application.Interfaces.Repository;
using Application.Wrappers.Results;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Meetings.Commands;

public class CreateMeeting : IRequest<IDataResult<Meeting>>
{
    public string? MeetingName { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string? Description { get; set; }

    public string? Document { get; set; }

    public class CreateMeetingHandler : IRequestHandler<CreateMeeting, IDataResult<Meeting>>
    {
        private readonly IMapper _mapper;
        private readonly IMeetingRepository _meetingRepository;

        public CreateMeetingHandler(IMeetingRepository MeetingRepository, IMapper mapper)
        {
            _meetingRepository = MeetingRepository;
            _mapper = mapper;
        }

        public async Task<IDataResult<Meeting>> Handle(CreateMeeting request, CancellationToken cancellationToken)
        {
            var meeting = _mapper.Map<Meeting>(request);
            await _meetingRepository.AddAsync(meeting);
            return new SuccessDataResult<Meeting>(meeting, "Meeting created");
        }
    }
}