using Domain.Model.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Contracts.Infra.Persistence;

public interface IUnitOfWork : IDisposable
{
    DbSet<User> Users { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}