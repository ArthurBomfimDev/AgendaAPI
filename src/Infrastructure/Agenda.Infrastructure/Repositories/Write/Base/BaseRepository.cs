using Agenda.Application.Interfaces.Repository.Write.Base;
using Agenda.Domain.Entities.Base;
using Agenda.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Infrastructure.Repositories.Write.Base;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : BaseEntity<TEntity>
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public BaseRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public virtual async Task<TEntity?> GetById(long id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task Create(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public virtual void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public virtual void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }
}