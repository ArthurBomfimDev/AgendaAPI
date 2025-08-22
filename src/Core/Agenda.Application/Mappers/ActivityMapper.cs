using Agenda.Application.ViewModels.DTO.Activity;
using Agenda.Domain.Entities;

namespace Agenda.Application.Mappers;

public static class ActivityMapper
{
    public static ActivityDTO? ToDTO(this Activity? activity)
    {
        return activity == null ? null :
            new ActivityDTO(activity.Id,
                                activity.Title,
                                activity.Description,
                                activity.Status,
                                activity.Priority,
                                activity.DueDate,
                                activity.ActualStartDate,
                                activity.ActualCompletionDate,
                                activity.ActualCancellationDate,
                                activity.ElapsedSinceCreation,
                                activity.TimeToStart,
                                activity.FinalWorkedTime,
                                activity.DelayDuration,
                                activity.IsOverdue,
                                activity.RemainingTime,
                                activity.ElapsedSinceCreationNow,
                                activity.WorkedDurationUntilNow,
                                activity.DelayDurationUntilNow,
                                activity.CreatedDate,
                                activity.ChangedDate,
                                activity.IsActive);
    }
}