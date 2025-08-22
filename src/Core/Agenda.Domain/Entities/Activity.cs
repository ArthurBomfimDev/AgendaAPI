using Agenda.Domain.Entities.Base;
using Agenda.Domain.Enuns.ActivityPriority;
using Agenda.Domain.Enuns.ActivityStatus;
using Agenda.Domain.Exceptions;

namespace Agenda.Domain.Entities;

public class Activity : BaseEntity<Activity>
{
    public string Title { get; private set; }
    public string? Description { get; private set; }

    public EnumActivityStatus Status { get; private set; }
    public EnumActivityPriority Priority { get; private set; }

    public DateTimeOffset? DueDate { get; private set; }
    public DateTimeOffset? ActualStartDate { get; private set; }
    public DateTimeOffset? ActualCompletionDate { get; private set; }
    public DateTimeOffset? ActualCancellationDate { get; private set; }

    public TimeSpan? ElapsedSinceCreation { get; private set; }
    public TimeSpan? TimeToStart { get; private set; }
    public TimeSpan? FinalWorkedTime { get; private set; }
    public TimeSpan? DelayDuration { get; private set; }

    #region Time Log
    private readonly List<ActivityTimeLog> _timeLogs = new();
    public IReadOnlyCollection<ActivityTimeLog> TimeLogs => _timeLogs.AsReadOnly();
    #endregion

    #region Constructors
    private Activity() { }

    private Activity(string title, string? description, EnumActivityPriority priority, DateTimeOffset? dueDate) : base()
    {
        Title = title;
        Description = description;
        Priority = priority;
        DueDate = dueDate;
        Status = EnumActivityStatus.Pendente;
    }
    #endregion

    #region Create
    public static Activity Create(string title, string? description, EnumActivityPriority priority, DateTimeOffset? dueDate)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainException("O título da atividade não pode ser vazio.");

        return new Activity(title, description, priority, dueDate);
    }
    #endregion

    #region Update
    public void Update(string title, string? description, EnumActivityPriority priority, DateTimeOffset? dueDate)
    {
        if (Status == EnumActivityStatus.Concluido || Status == EnumActivityStatus.Cancelado)
            throw new DomainException($"Não é possível atualizar uma atividade com Status {Status}, encerramento em: {(Status == EnumActivityStatus.Concluido ? ActualCompletionDate : ActualCancellationDate)}");

        if (string.IsNullOrWhiteSpace(title))
            throw new DomainException("O título da atividade não pode ser vazio.");

        Title = title;
        Description = description;
        Priority = priority;
        DueDate = dueDate;

        SetChangedDate();
    }

    public void UpdatePriority(EnumActivityPriority priority)
    {
        if (Status == EnumActivityStatus.Concluido || Status == EnumActivityStatus.Cancelado)
            throw new DomainException($"Não é possível atualizar uma atividade com Status {Status}, encerramento em: {(Status == EnumActivityStatus.Concluido ? ActualCompletionDate : ActualCancellationDate)}");

        Priority = priority;
        SetChangedDate();
    }

    public void UpdateDueDate(DateTimeOffset? dueDate)
    {
        if (Status == EnumActivityStatus.Concluido || Status == EnumActivityStatus.Cancelado)
            throw new DomainException($"Não é possível atualizar uma atividade com Status {Status}, encerramento em: {(Status == EnumActivityStatus.Concluido ? ActualCompletionDate : ActualCancellationDate)}");

        DueDate = dueDate;
        SetChangedDate();
    }
    #endregion

    #region Start/Pause/Complete/Cancel
    public void Start()
    {
        if (Status == EnumActivityStatus.Concluido || Status == EnumActivityStatus.Cancelado)
            throw new DomainException($"Não é possível iniciar uma atividade com Status: {Status}, encerramento em: {(Status == EnumActivityStatus.Concluido ? ActualCompletionDate : ActualCancellationDate)}");

        if (Status == EnumActivityStatus.Andamento)
            throw new DomainException($"A atividade já está em andamento, foi aberta em: {_timeLogs.LastOrDefault()!.StartTime}");

        var now = DateTimeOffset.UtcNow;

        if (!ActualStartDate.HasValue)
        {
            ActualStartDate = now;
            TimeToStart = now - CreatedDate;
        }

        _timeLogs.Add(new ActivityTimeLog(now));
        Status = EnumActivityStatus.Andamento;
        SetChangedDate();
    }

    public void Pause()
    {
        if (Status == EnumActivityStatus.Concluido || Status == EnumActivityStatus.Cancelado)
            throw new DomainException($"Não é possível pausar uma atividade com Status: {Status}, encerramento em: {(Status == EnumActivityStatus.Concluido ? ActualCompletionDate : ActualCancellationDate)}");

        if (Status != EnumActivityStatus.Andamento)
            throw new DomainException($"A atividade já foi pausada, foi pausada em: {_timeLogs.LastOrDefault()!.EndTime}");

        var now = DateTimeOffset.UtcNow;
        var open = _timeLogs.LastOrDefault(t => t.IsOpen);

        if (open != null) open.End(now);

        Status = EnumActivityStatus.Pausado;
        SetChangedDate();
    }

    public void Complete()
    {
        if (Status == EnumActivityStatus.Cancelado)
            throw new DomainException($"A Atividade já foi cancelada em: {ActualCancellationDate} e não pode ser concluida");

        if (Status == EnumActivityStatus.Concluido)
            throw new DomainException($"A Atividade já foi concluida, em: {ActualCompletionDate}");

        var now = DateTimeOffset.UtcNow;
        var open = _timeLogs.LastOrDefault(t => t.IsOpen);
        if (open != null) open.End(now);

        ActualCompletionDate = now;
        Status = EnumActivityStatus.Concluido;
        ElapsedSinceCreation = CalculateElapsedSinceCreation(now);
        FinalWorkedTime = CalculateWorkedDuration(now);
        DelayDuration = CalculateDelayDuration(now);
        SetChangedDate();
    }

    public void Cancel()
    {
        if (Status == EnumActivityStatus.Cancelado)
            throw new DomainException($"A Atividade já foi cancelada em: {ActualCancellationDate}");

        if (Status == EnumActivityStatus.Concluido)
            throw new DomainException($"A Atividade já foi concluida em: {ActualCompletionDate} e não pode ser cancelada");

        var now = DateTimeOffset.UtcNow;
        var open = _timeLogs.LastOrDefault(t => t.IsOpen);
        if (open != null) open.End(now);

        ActualCancellationDate = now;
        Status = EnumActivityStatus.Cancelado;
        ElapsedSinceCreation = CalculateElapsedSinceCreation(now);
        FinalWorkedTime = CalculateWorkedDuration(now);
        DelayDuration = CalculateDelayDuration(now);
        SetChangedDate();
    }
    #endregion

    #region Time queries
    public bool IsOverdue
    {
        get
        {
            if (!DueDate.HasValue) return false;
            if (ActualCompletionDate.HasValue) return ActualCompletionDate.Value > DueDate.Value;
            return Status != EnumActivityStatus.Concluido && DateTimeOffset.UtcNow > DueDate.Value;
        }
    }

    public TimeSpan? RemainingTime => CalculateRemainingTime(DateTimeOffset.UtcNow);
    public TimeSpan ElapsedSinceCreationNow => CalculateElapsedSinceCreation(DateTimeOffset.UtcNow);

    public TimeSpan WorkedDurationUntilNow => CalculateWorkedDuration(DateTimeOffset.UtcNow);

    public TimeSpan DelayDurationUntilNow => CalculateDelayDuration(DateTimeOffset.UtcNow);

    private TimeSpan CalculateElapsedSinceCreation(DateTimeOffset pointInTime) => pointInTime - CreatedDate;

    private TimeSpan? CalculateRemainingTime(DateTimeOffset pointInTime)
    {
        return DueDate.HasValue ? DueDate - pointInTime : null;
    }

    private TimeSpan CalculateWorkedDuration(DateTimeOffset pointInTime)
    {
        return TimeSpan.FromHours(_timeLogs.Sum(log =>
        {
            var endTime = log.EndTime ?? pointInTime;
            return (endTime - log.StartTime).TotalHours;
        }));
    }

    private TimeSpan CalculateDelayDuration(DateTimeOffset pointInTime)
    {
        var completionDate = ActualCompletionDate ?? pointInTime;
        if (DueDate.HasValue && completionDate > DueDate.Value)
        {
            return completionDate - DueDate.Value;
        }
        return TimeSpan.Zero;
    }


    #endregion
}