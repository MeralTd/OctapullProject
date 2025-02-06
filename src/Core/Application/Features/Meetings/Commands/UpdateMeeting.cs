using Application.Interfaces.Repository;
using Application.Wrappers.Results;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Meetings.Commands;

public class UpdateMeeting : IRequest<IResponseResult>
{
    public int Id { get; set; }
    public string? MeetingName { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string? Description { get; set; }

    public string? Document { get; set; }

    public class UpdateMeetingHandler : IRequestHandler<UpdateMeeting, IResponseResult>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMapper _mapper;

        public UpdateMeetingHandler(IMeetingRepository meetingRepository, IMapper mapper)
        {
            _meetingRepository = meetingRepository;
            _mapper = mapper;
        }

        public async Task<IResponseResult> Handle(UpdateMeeting request, CancellationToken cancellationToken)
        {
            var meeting = await _meetingRepository.GetAsync(x => x.Id == request.Id);
            if (meeting == null)
                return new ErrorResult("Meeting not found");

            var meetingMap = _mapper.Map<Meeting>(request);
            meetingMap.UpdatedDate = DateTime.UtcNow;
            await _meetingRepository.UpdateAsync(meetingMap);
            return new SuccessResult("Meeting updated");
        }
    }
}