using Domain.Common;

namespace Domain.Entities;

public class Meeting : BaseEntity
{
    public string? MeetingName { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string? Description { get; set; }

    public string? Document { get; set; }
}
