using Agenda.Application.ViewModels.DTO.Activity;
using Agenda.Domain.Enuns.ActivityStatus;

namespace Agenda.Application.Interfaces.Repository.Read;

public interface IActivityReadRepository
{
    Task<IEnumerable<ActivityDTO>> GetAll();
    Task<ActivityDTO?> GetById(long id);
    Task<IEnumerable<ActivityDTO>> GetByDueDateRange(DateTimeOffset? dueDateFrom, DateTimeOffset? dueDateTo);
    Task<IEnumerable<ActivityDTO>> GetByTitle(string title);
    Task<IEnumerable<ActivityDTO>> GetByStatus(EnumActivityStatus status);
}