using Agenda.Application.Interfaces.Repository.Read;
using Agenda.Application.ViewModels.DTO.Activity;
using Agenda.Domain.Enuns.ActivityStatus;
using Agenda.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Infrastructure.Repositories.Read;

public class ActivityReadRepository : IActivityReadRepository
{
    private readonly AppDbContext _context;

    public ActivityReadRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ActivityDTO>> GetAll()
    {
        var now = DateTimeOffset.UtcNow;

        var activities = await _context.Activities
                                .AsNoTracking()
                                .Select(a => new
                                {
                                    a.Id,
                                    a.Title,
                                    a.Description,
                                    a.Status,
                                    a.Priority,
                                    a.DueDate,
                                    a.ActualStartDate,
                                    a.ActualCompletionDate,
                                    a.ActualCancellationDate,
                                    a.ElapsedSinceCreation,
                                    a.TimeToStart,
                                    a.FinalWorkedTime,
                                    a.DelayDuration,
                                    a.CreatedDate,
                                    a.ChangedDate,
                                    a.IsActive,
                                    TimeLogs = a.TimeLogs.Select(t => new { t.StartTime, t.EndTime }).ToList()
                                })
                                .ToListAsync();

        return activities.Select(a =>
        {
            var elapsedSinceCreationNow = now - a.CreatedDate;

            var workedUntilNow = TimeSpan.FromHours(
                a.TimeLogs.Sum(t => ((t.EndTime ?? now) - t.StartTime).TotalHours));

            bool isOverdue = a.DueDate.HasValue ? a.Status != EnumActivityStatus.Concluido && a.Status != EnumActivityStatus.Cancelado ? now > a.DueDate : false : false;

            var delayUntilNow = isOverdue ? now - a.DueDate!.Value : TimeSpan.Zero;

            return new ActivityDTO
            (
                a.Id,
                a.Title,
                a.Description,
                a.Status,
                a.Priority,
                a.DueDate,
                a.ActualStartDate,
                a.ActualCompletionDate,
                a.ActualCancellationDate,
                a.ElapsedSinceCreation,
                a.TimeToStart,
                a.FinalWorkedTime,
                a.DelayDuration,
                isOverdue,
                elapsedSinceCreationNow,
                workedUntilNow,
                delayUntilNow,
                a.CreatedDate,
                a.ChangedDate,
                a.IsActive
            );
        }).ToList();
    }

    public async Task<IEnumerable<ActivityDTO>> GetByDueDateRange(DateTimeOffset? dueDateFrom, DateTimeOffset? dueDateTo)
    {
        var now = DateTimeOffset.UtcNow;

        var queries = _context.Activities
            .Where(a => a.DueDate.HasValue)
            .AsNoTracking();

        if (dueDateFrom.HasValue)
            queries = queries.Where(a => a.DueDate >= dueDateFrom);

        if (dueDateTo.HasValue)
            queries = queries.Where(a => a.DueDate <= dueDateTo);

        var listActivity = await queries
            .OrderBy(a => a.DueDate)
            .Select(a => new
            {
                a.Id,
                a.Title,
                a.Description,
                a.Status,
                a.Priority,
                a.DueDate,
                a.ActualStartDate,
                a.ActualCompletionDate,
                a.ActualCancellationDate,
                a.ElapsedSinceCreation,
                a.TimeToStart,
                a.FinalWorkedTime,
                a.DelayDuration,
                a.CreatedDate,
                a.ChangedDate,
                a.IsActive,
                TimeLogs = a.TimeLogs.Select(t => new { t.StartTime, t.EndTime }).ToList()
            })
            .ToListAsync();

        return listActivity.Select(a =>
        {
            var elapsedSinceCreationNow = now - a.CreatedDate;

            var workedUntilNow = TimeSpan.FromHours(
                a.TimeLogs.Sum(t => ((t.EndTime ?? now) - t.StartTime).TotalHours));

            bool isOverdue = a.DueDate.HasValue ? a.Status != EnumActivityStatus.Concluido && a.Status != EnumActivityStatus.Cancelado ? now > a.DueDate : false : false;

            var delayUntilNow = isOverdue ? now - a.DueDate!.Value : TimeSpan.Zero;

            return new ActivityDTO
            (
                a.Id,
                a.Title,
                a.Description,
                a.Status,
                a.Priority,
                a.DueDate,
                a.ActualStartDate,
                a.ActualCompletionDate,
                a.ActualCancellationDate,
                a.ElapsedSinceCreation,
                a.TimeToStart,
                a.FinalWorkedTime,
                a.DelayDuration,
                isOverdue,
                elapsedSinceCreationNow,
                workedUntilNow,
                delayUntilNow,
                a.CreatedDate,
                a.ChangedDate,
                a.IsActive
            );
        }).ToList();
    }

    public async Task<ActivityDTO?> GetById(long id)
    {
        var now = DateTimeOffset.UtcNow;

        var query = await _context.Activities
            .Where(a => a.Id == id)
            .AsNoTracking()
            .Select(a => new
            {
                a.Id,
                a.Title,
                a.Description,
                a.Status,
                a.Priority,
                a.DueDate,
                a.ActualStartDate,
                a.ActualCompletionDate,
                a.ActualCancellationDate,
                a.ElapsedSinceCreation,
                a.TimeToStart,
                a.FinalWorkedTime,
                a.DelayDuration,
                a.CreatedDate,
                a.ChangedDate,
                a.IsActive,
                TimeLogs = a.TimeLogs.Select(t => new { t.StartTime, t.EndTime }).ToList()
            }).FirstOrDefaultAsync();

        if (query == null)
            return null;

        var elapsedSinceCreationNow = now - query.CreatedDate;

        var workedUntilNow = TimeSpan.FromHours(
            query.TimeLogs.Sum(t => ((t.EndTime ?? now) - t.StartTime).TotalHours));

        bool isOverdue = query.DueDate.HasValue ? query.Status != EnumActivityStatus.Concluido && query.Status != EnumActivityStatus.Cancelado ? now > query.DueDate : false : false;

        var delayUntilNow = isOverdue ? now - query.DueDate!.Value : TimeSpan.Zero;

        return new ActivityDTO(
                query.Id,
                query.Title,
                query.Description,
                query.Status,
                query.Priority,
                query.DueDate,
                query.ActualStartDate,
                query.ActualCompletionDate,
                query.ActualCancellationDate,
                query.ElapsedSinceCreation,
                query.TimeToStart,
                query.FinalWorkedTime,
                query.DelayDuration,
                isOverdue,
                elapsedSinceCreationNow,
                workedUntilNow,
                delayUntilNow,
                query.CreatedDate,
                query.ChangedDate,
                query.IsActive
            );
    }

    public async Task<IEnumerable<ActivityDTO>> GetByStatus(EnumActivityStatus status)
    {
        var now = DateTimeOffset.UtcNow;

        var queries = await _context.Activities
            .Where(a => a.Status == status)
            .AsNoTracking()
            .Select(a => new
            {
                a.Id,
                a.Title,
                a.Description,
                a.Status,
                a.Priority,
                a.DueDate,
                a.ActualStartDate,
                a.ActualCompletionDate,
                a.ActualCancellationDate,
                a.ElapsedSinceCreation,
                a.TimeToStart,
                a.FinalWorkedTime,
                a.DelayDuration,
                a.CreatedDate,
                a.ChangedDate,
                a.IsActive,
                TimeLogs = a.TimeLogs.Select(t => new { t.StartTime, t.EndTime }).ToList()
            })
            .ToListAsync();

        return queries.Select(a =>
        {
            var elapsedSinceCreationNow = now - a.CreatedDate;

            var workedUntilNow = TimeSpan.FromHours(
                a.TimeLogs.Sum(t => ((t.EndTime ?? now) - t.StartTime).TotalHours));

            bool isOverdue = a.DueDate.HasValue ? a.Status != EnumActivityStatus.Concluido && a.Status != EnumActivityStatus.Cancelado ? now > a.DueDate : false : false;

            var delayUntilNow = isOverdue ? now - a.DueDate!.Value : TimeSpan.Zero;

            return new ActivityDTO
            (
                a.Id,
                a.Title,
                a.Description,
                a.Status,
                a.Priority,
                a.DueDate,
                a.ActualStartDate,
                a.ActualCompletionDate,
                a.ActualCancellationDate,
                a.ElapsedSinceCreation,
                a.TimeToStart,
                a.FinalWorkedTime,
                a.DelayDuration,
                isOverdue,
                elapsedSinceCreationNow,
                workedUntilNow,
                delayUntilNow,
                a.CreatedDate,
                a.ChangedDate,
                a.IsActive
            );
        }).ToList();
    }

    public async Task<IEnumerable<ActivityDTO>> GetByTitle(string title)
    {
        var now = DateTimeOffset.UtcNow;

        var formatedTitle = title.Trim().ToLower();

        var queries = await _context.Activities
            .Where(a => a.Title.ToLower().Contains(formatedTitle))
            .AsNoTracking()
            .Select(a => new
            {
                a.Id,
                a.Title,
                a.Description,
                a.Status,
                a.Priority,
                a.DueDate,
                a.ActualStartDate,
                a.ActualCompletionDate,
                a.ActualCancellationDate,
                a.ElapsedSinceCreation,
                a.TimeToStart,
                a.FinalWorkedTime,
                a.DelayDuration,
                a.CreatedDate,
                a.ChangedDate,
                a.IsActive,
                TimeLogs = a.TimeLogs.Select(t => new { t.StartTime, t.EndTime }).ToList()
            })
            .ToListAsync();

        return queries.Select(a =>
        {
            var elapsedSinceCreationNow = now - a.CreatedDate;

            var workedUntilNow = TimeSpan.FromHours(
                a.TimeLogs.Sum(t => ((t.EndTime ?? now) - t.StartTime).TotalHours));

            bool isOverdue = a.DueDate.HasValue ? a.Status != EnumActivityStatus.Concluido && a.Status != EnumActivityStatus.Cancelado ? now > a.DueDate : false : false;

            var delayUntilNow = isOverdue ? now - a.DueDate!.Value : TimeSpan.Zero;

            return new ActivityDTO
            (
                a.Id,
                a.Title,
                a.Description,
                a.Status,
                a.Priority,
                a.DueDate,
                a.ActualStartDate,
                a.ActualCompletionDate,
                a.ActualCancellationDate,
                a.ElapsedSinceCreation,
                a.TimeToStart,
                a.FinalWorkedTime,
                a.DelayDuration,
                isOverdue,
                elapsedSinceCreationNow,
                workedUntilNow,
                delayUntilNow,
                a.CreatedDate,
                a.ChangedDate,
                a.IsActive
            );
        }).ToList();
    }
}