using Domain.Model.Suppliers;
using Domain.Model.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Contracts.Infra.Persistence;

public interface IUnitOfWork : IDisposable
{
    DbSet<Supplier> Suppliers { get; }
    DbSet<User> Users { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}