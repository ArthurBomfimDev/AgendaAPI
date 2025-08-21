using Agenda.Domain.Enuns.ActivityPriority;
using Agenda.Domain.Enuns.ActivityStatus;

namespace Agenda.Application.ViewModels.DTO.Activity;

public record ActivityDTO(
    long Id,
    string Title,
    string? Description,
    EnumActivityStatus Status,
    EnumActivityPriority Priority,
    DateTimeOffset? DueDate,
    DateTimeOffset? ActualStartDate,
    DateTimeOffset? ActualCompletionDate,
    DateTimeOffset? ActualCancellationDate,
    TimeSpan? ElapsedSinceCreation,
    TimeSpan? TimeToStart,
    TimeSpan? FinalWorkedTime,
    TimeSpan? DelayDuration,
    bool IsOverdue,
    TimeSpan ElapsedSinceCreationUntilNow,
    TimeSpan WorkedDurationUntilNow,
    TimeSpan DelayDurationUntilNow,
    DateTimeOffset CreatedDate,
    DateTimeOffset? ChangedDate,
    bool IsActive
);