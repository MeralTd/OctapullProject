using Application.Features.Users.Dtos;
using Application.Interfaces.Repository;
using Application.Wrappers.Results;
using AutoMapper;
using Domain.Entities;
using Mailing;
using MediatR;
using MimeKit;

namespace Application.Features.Meetings.Commands;

public class CreateMeeting : IRequest<IDataResult<Meeting>>
{
    public string? MeetingName { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string? Description { get; set; }

    public string? Document { get; set; }

    public UserDto? User { get; set; }

    public class CreateMeetingHandler : IRequestHandler<CreateMeeting, IDataResult<Meeting>>
    {
        private readonly IMapper _mapper;
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMailService _mailService;

        public CreateMeetingHandler(IMapper mapper, IMeetingRepository MeetingRepository, IMailService mailService)
        {
            _mapper = mapper;
            _meetingRepository = MeetingRepository;
            _mailService = mailService;
        }

        public async Task<IDataResult<Meeting>> Handle(CreateMeeting request, CancellationToken cancellationToken)
        {
            var meeting = _mapper.Map<Meeting>(request);
            await _meetingRepository.AddAsync(meeting);


            if (request.User.Email != null)
            {
                var toEmailList = new List<MailboxAddress>();


                toEmailList.Add(new MailboxAddress(name: request.User.FirstName + ' ' + request.User.LastName, address: request.User.Email));

                await _mailService.SendEmailAsync(new Mail
                {
                    ToList = toEmailList,
                    BccList = null,
                    Subject = "A Meeting has been created",
                    HtmlBody = $@"
                    <html>
                        <body>
                            <p>Hello {request.User.FirstName + ' ' + request.User.LastName},</p>
                            <p>A meeting has been created! Meeting details are provided below: </p>
                            <p><strong>Meeting Topic:</strong> {meeting.MeetingName}</p>
                            <p><strong>Date and Time:</strong> {meeting.StartDate}</p>
                            <p><strong>Explanation:</strong>{meeting.Description}</p>
                        </body>
                    </html>"
                });
            }

            return new SuccessDataResult<Meeting>(meeting, "Meeting created");
        }
    }
}