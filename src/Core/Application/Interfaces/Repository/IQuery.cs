namespace Application.Interfaces.Repository;

public interface IQuery<T>
{
    IQueryable<T> Query();
}
