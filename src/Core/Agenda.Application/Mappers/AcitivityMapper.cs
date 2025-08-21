using Agenda.Application.ViewModels.DTO.Activity;
using Agenda.Domain.Entities;

namespace Agenda.Application.Mappers;

public static class ActivityMapper
{
    public static ActivityDTO? ToDTO(this Activity? acitivity)
    {
        return acitivity == null ? null :
            new ActivityDTO(acitivity.Id,
                                acitivity.Title,
                                acitivity.Description,
                                acitivity.Status,
                                acitivity.Priority,
                                acitivity.DueDate,
                                acitivity.ActualStartDate,
                                acitivity.ActualCompletionDate,
                                acitivity.ActualCancellationDate,
                                acitivity.ElapsedSinceCreation,
                                acitivity.TimeToStart,
                                acitivity.FinalWorkedTime,
                                acitivity.DelayDuration,
                                acitivity.IsOverdue,
                                acitivity.ElapsedSinceCreationNow,
                                acitivity.WorkedDurationUntilNow,
                                acitivity.DelayDurationUntilNow,
                                acitivity.CreatedDate,
                                acitivity.ChangedDate,
                                acitivity.IsActive);
    }
}