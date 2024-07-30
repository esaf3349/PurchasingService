using Domain.Model.BudgetLines;
using Domain.Model.Currencies;
using Domain.Model.Departments;
using Domain.Model.EntityChanges;
using Domain.Model.Goods;
using Domain.Model.Measures;
using Domain.Model.RequisitionLines;
using Domain.Model.Requisitions;
using Domain.Model.Roles;
using Domain.Model.Suppliers;
using Domain.Model.Users;
using Domain.Model.Warehouses;
using Microsoft.EntityFrameworkCore;

namespace Application.Contracts.Infra.Persistence;

public interface IUnitOfWork : IDisposable
{
    DbSet<BudgetLine> BudgetLines { get; }
    DbSet<Currency> Currencies { get; }
    DbSet<Department> Departments { get; }
    DbSet<EntityChange> EntityChanges { get; }
    DbSet<Good> Goods { get; }
    DbSet<Measure> Measures { get; }
    DbSet<Requisition> Requisitions { get; }
    DbSet<RequisitionLine> RequisitionLines { get; }
    DbSet<Role> Roles { get; }
    DbSet<Supplier> Suppliers { get; }
    DbSet<User> Users { get; }
    DbSet<Warehouse> Warehouses { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}