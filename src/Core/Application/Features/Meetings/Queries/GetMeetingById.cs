using Application.Features.Meetings.Dtos;
using Application.Interfaces.Repository;
using Application.Wrappers.Results;
using AutoMapper;
using MediatR;

namespace Application.Features.Meetings.Queries;

public class GetMeetingById : IRequest<IDataResult<MeetingDto>>
{
    public int Id { get; set; }

    public class GetMeetingByIdHandler : IRequestHandler<GetMeetingById, IDataResult<MeetingDto>>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMapper _mapper;

        public GetMeetingByIdHandler(IMeetingRepository meetingRepository, IMapper mapper)
        {
            _meetingRepository = meetingRepository;
            _mapper = mapper;
        }

        public async Task<IDataResult<MeetingDto>> Handle(GetMeetingById request, CancellationToken cancellationToken)
        {
            var meeting = await _meetingRepository.GetAsync(x => x.Id == request.Id && x.IsCancelled == false);
            if (meeting == null)
                return new ErrorDataResult<MeetingDto>("Meeting not found");

            var mappedModel = _mapper.Map<MeetingDto>(meeting);
            return new SuccessDataResult<MeetingDto>(mappedModel);
        }
    }
}