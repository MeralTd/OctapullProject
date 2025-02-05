namespace Domain.Entities;

public class MeetingDeleteLog
{
    public int Id { get; set; }
    public int? MeetingId { get; set; }
    public string? MeetingName { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Description { get; set; }
    public string? Document { get; set; }
    public DateTime? DeletedDate { get; set; }
}