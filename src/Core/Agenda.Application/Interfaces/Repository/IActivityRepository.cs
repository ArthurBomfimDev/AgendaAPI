using Agenda.Domain.Entities;

namespace Agenda.Application.Interfaces.Repository;

public interface IActivityRepository
{
    Task<Activity> GetById(long id);
    Task<IEnumerable<Activity>> GetAll();
    Task Create(Activity activity);
    Task Update(Activity activity);
    Task Delete(Activity activity);
}