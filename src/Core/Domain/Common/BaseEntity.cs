namespace Domain.Common;

public class BaseEntity : IEntity
{
    public int Id { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
}