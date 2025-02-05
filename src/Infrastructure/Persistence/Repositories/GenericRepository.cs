using Application.Interfaces.Repository;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Persistence.Repositories;

public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity> where TEntity : BaseEntity, new() where TContext : DbContext, new()
{

    public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
    {
        try
        {
            using TContext context = new();
            return filter == null
                ? context.Set<TEntity>().AsQueryable()
                : context.Set<TEntity>().Where(filter).AsQueryable();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public virtual async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null)
    {
        try
        {
            await using TContext context = new();
            return filter == null
                ? await context.Set<TEntity>().ToListAsync()
                : await context.Set<TEntity>().Where(filter).ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter = null, CancellationToken cancellationToken = default)
    {
        try
        {
            await using TContext context = new();
            return filter == null
                ? await context.Set<TEntity>().FirstOrDefaultAsync(cancellationToken)
                : await context.Set<TEntity>().FirstOrDefaultAsync(filter, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public virtual async Task AddAsync(TEntity entity)
    {
        try
        {
            await using TContext context = new();
            context.Add(entity);
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        try
        {
            await using TContext context = new();
            context.Update(entity);
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public virtual async Task RemoveAsync(TEntity entity)
    {
        try
        {
            await using TContext context = new();
            context.Remove(entity);
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

}