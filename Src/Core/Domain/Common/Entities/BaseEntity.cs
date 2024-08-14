using Domain.Common.Time;

namespace Domain.Common.Entities;

public abstract class BaseEntity<TEntityId> : IAuditableEntity, ISoftDeletableEntity where TEntityId : struct
{
    protected BaseEntity()
    {
        IsActive = true;

        var now = MachineDateTime.Now;
        CreatedAt = now;
        UpdatedAt = now;
    }

    protected BaseEntity(TEntityId id) : this() => Id = id;

    public TEntityId Id { get; private init; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private init; }
    public DateTime UpdatedAt { get; private set; }

    public void RefreshUpdatedAt() => UpdatedAt = MachineDateTime.Now;

    public void Delete() => IsActive = false;
}