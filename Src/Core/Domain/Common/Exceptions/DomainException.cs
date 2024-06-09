using Domain.Common.Entities;

namespace Domain.Common.Exceptions;

public class DomainException<TEntity> : Exception, IDomainException where TEntity : IAuditableEntity
{
    public DomainException(string message) : base($"{typeof(TEntity).Name}: {message}")
    {

    }
}