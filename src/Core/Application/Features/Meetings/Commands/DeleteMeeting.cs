using Application.Interfaces.Repository;
using Application.Wrappers.Results;
using AutoMapper;
using MediatR;

namespace Application.Features.Meetings.Commands;

public class DeleteMeeting : IRequest<IResponseResult>
{
    public int Id { get; set; }

    public class DeleteMeetingHandler : IRequestHandler<DeleteMeeting, IResponseResult>
    {
        private readonly IMapper _mapper;
        private readonly IMeetingRepository _meetingRepository;

        public DeleteMeetingHandler(IMapper mapper, IMeetingRepository meetingRepository)
        {
            _mapper = mapper;
            _meetingRepository = meetingRepository;
        }

        public async Task<IResponseResult> Handle(DeleteMeeting request, CancellationToken cancellationToken)
        {
            var meeting = await _meetingRepository.GetAsync(x => x.Id == request.Id);
            if (meeting == null)
                return new ErrorResult("Meeting not found");



            meeting.IsCancelled = true;
            meeting.IsCancelledDate = DateTime.UtcNow;
            await _meetingRepository.UpdateAsync(meeting);

            //await _meetingRepository.RemoveAsync(meeting);
            return new SuccessResult("Meeting deleted");
        }
    }
}