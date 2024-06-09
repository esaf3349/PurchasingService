namespace Domain.Common.Entities;

public interface ISoftDeletableEntity
{
    bool IsActive { get; }
    void Delete();
}