using Agenda.Domain.Entities.Base;

namespace Agenda.Application.Interfaces.Repository.Write.Base;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity<TEntity>
{
    Task<TEntity?> GetById(long id);
    Task Create(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}