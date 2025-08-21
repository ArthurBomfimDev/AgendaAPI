using Agenda.Application.Interfaces.Repository.Write;
using Agenda.Domain.Entities;
using Agenda.Infrastructure.Persistence;
using Agenda.Infrastructure.Repositories.Write.Base;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Infrastructure.Repositories.Write;

public class ActivityRepository : BaseRepository<Activity>, IActivityRepository
{
    public ActivityRepository(AppDbContext context) : base(context)
    {
    }

    public override async Task<Activity?> GetById(long id)
    {
        return await _context.Activities
            .Include(a => a.TimeLogs)
            .FirstOrDefaultAsync(a => a.Id == id);
    }
}