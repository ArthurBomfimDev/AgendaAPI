using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agenda.Domain.Entities.Base;

public abstract class BaseEntity<TEntity> where TEntity : BaseEntity<TEntity>
{
    public long Id { get; protected set; }
    public DateTimeOffset CreatedDate { get; protected set; }
    public DateTimeOffset? ChangedDate { get; protected set; }
    public bool IsActive { get; protected set; }
    private readonly List<INotification> _domainEvents = new();

    [NotMapped]
    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

    protected BaseEntity()
    {
        CreatedDate = DateTimeOffset.UtcNow;
        IsActive = true;
    }

    #region methods
    public virtual void SetChangedDate()
    {
        ChangedDate = DateTimeOffset.UtcNow;
    }

    public void AddDomainEvent(INotification eventItem)
    {
        _domainEvents.Add(eventItem);
    }

    public void RemoveDomainEvent(INotification eventItem)
    {
        _domainEvents?.Remove(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }

    public virtual bool Deactivate()
    {
        if (IsActive)
        {
            IsActive = false;
            SetChangedDate();

            return true;
        }

        return false;
    }

    public virtual bool Activate()
    {
        if (!IsActive)
        {
            IsActive = true;
            SetChangedDate();

            return true;
        }

        return false;
    }
    #endregion
}