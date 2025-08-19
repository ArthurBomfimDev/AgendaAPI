using Agenda.Domain.Enuns.ActivityStatus;

namespace Agenda.Domain.Entities;

public class Activity
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime DueDate { get; set; }
    public EnumActivityStatus Status { get; set; } 

    public bool IsOverdue()
    {
        return DueDate < DateTime.Now && Status != EnumActivityStatus.Finalizado;
    }
}