namespace Application.Features.Meetings.Dtos;

public class MeetingDto
{
    public int Id { get; set; }
    public string? MeetingName { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string? Description { get; set; }

    public string? Document { get; set; }
}