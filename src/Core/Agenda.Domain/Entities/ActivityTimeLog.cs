using Agenda.Domain.Exceptions;

namespace Agenda.Domain.Entities;

public class ActivityTimeLog
{
    public DateTimeOffset StartTime { get; private set; }
    public DateTimeOffset? EndTime { get; private set; }
    public TimeSpan? Duration => (EndTime ?? DateTimeOffset.UtcNow) - StartTime;
    public bool IsOpen => !EndTime.HasValue;


    private ActivityTimeLog() { }

    public ActivityTimeLog(DateTimeOffset startTime)
    {
        StartTime = startTime;
    }

    public void End(DateTimeOffset endTime)
    {
        if (EndTime.HasValue) return;
        if (endTime < StartTime) throw new DomainException("A hora de finalização não pode ser anterior a hora de inicio");

        EndTime = endTime;
    }
}