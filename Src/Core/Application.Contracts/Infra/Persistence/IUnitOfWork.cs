using Domain.Model.BudgetLines;
using Domain.Model.Currencies;
using Domain.Model.Departments;
using Domain.Model.Goods;
using Domain.Model.Measures;
using Domain.Model.Suppliers;
using Domain.Model.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Contracts.Infra.Persistence;

public interface IUnitOfWork : IDisposable
{
    DbSet<BudgetLine> BudgetLines { get; }
    DbSet<Currency> Currencies { get; }
    DbSet<Department> Departments { get; }
    DbSet<Good> Goods { get; }
    DbSet<Measure> Measures { get; }
    DbSet<Supplier> Suppliers { get; }
    DbSet<User> Users { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}