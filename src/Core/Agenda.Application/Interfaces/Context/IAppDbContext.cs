using Agenda.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Application.Interfaces.Context;

public interface IAppDbContext
{
    DbSet<Activity> Activities { get; }
}