using Domain.Common;
using System.Linq.Expressions;

namespace Application.Interfaces.Repository;

public interface IGenericRepository<T> where T : BaseEntity
{
    IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null);
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);
    Task<T> GetAsync(Expression<Func<T, bool>> filter = null, CancellationToken cancellationToken = default);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task RemoveAsync(T entity);

}