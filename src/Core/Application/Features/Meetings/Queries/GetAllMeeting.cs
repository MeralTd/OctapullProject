using Application.Features.Meetings.Dtos;
using Application.Interfaces.Repository;
using Application.Wrappers.Results;
using AutoMapper;
using MediatR;

namespace Application.Features.Meetings.Queries;

public class GetAllMeeting : IRequest<IDataResult<IEnumerable<MeetingDto>>>
{
    public class GetAllMeetingHandler : IRequestHandler<GetAllMeeting, IDataResult<IEnumerable<MeetingDto>>>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMapper _mapper;

        public GetAllMeetingHandler(IMeetingRepository meetingRepository, IMapper mapper)
        {
            _meetingRepository = meetingRepository;
            _mapper = mapper;
        }

        public async Task<IDataResult<IEnumerable<MeetingDto>>> Handle(GetAllMeeting request, CancellationToken cancellationToken)
        {
            var meetings = await _meetingRepository.GetAllAsync();
            if (meetings.Count <= 0)
                return new ErrorDataResult<IEnumerable<MeetingDto>>("Meeting not found");

            var mappedModel = _mapper.Map<IEnumerable<MeetingDto>>(meetings);
            return new SuccessDataResult<IEnumerable<MeetingDto>>(mappedModel);
        }
    }
}