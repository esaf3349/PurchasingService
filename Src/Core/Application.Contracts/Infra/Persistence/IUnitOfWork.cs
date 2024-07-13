using Domain.Model.Departments;
using Domain.Model.Goods;
using Domain.Model.Suppliers;
using Domain.Model.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Contracts.Infra.Persistence;

public interface IUnitOfWork : IDisposable
{
    DbSet<Department> Departments { get; }
    DbSet<Good> Goods { get; }
    DbSet<Supplier> Suppliers { get; }
    DbSet<User> Users { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}