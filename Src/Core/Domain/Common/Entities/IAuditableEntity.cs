namespace Domain.Common.Entities;

public interface IAuditableEntity
{
    DateTime CreatedAt { get; }
    DateTime UpdatedAt { get; }
    void RefreshUpdatedAt();
}