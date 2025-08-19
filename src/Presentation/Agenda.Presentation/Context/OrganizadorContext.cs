using Agenda.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace TrilhaApiDesafio.Context
{
    public class OrganizadorContext : DbContext
    {
        public OrganizadorContext(DbContextOptions<OrganizadorContext> options) : base(options)
        {
            
        }

        public DbSet<Activity> Activitys { get; set; }
    }
}