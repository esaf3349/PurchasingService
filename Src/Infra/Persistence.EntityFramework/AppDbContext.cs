using Application.Contracts.Infra.Persistence;
using Application.Contracts.Presentation.CurrentUser;
using Domain.Common.Entities;
using Domain.Common.Guids;
using Domain.Model.BudgetLines;
using Domain.Model.Currencies;
using Domain.Model.Departments;
using Domain.Model.EntityChanges;
using Domain.Model.Goods;
using Domain.Model.Measures;
using Domain.Model.RequisitionLines;
using Domain.Model.Requisitions;
using Domain.Model.Permissions;
using Domain.Model.Roles;
using Domain.Model.Suppliers;
using Domain.Model.Users;
using Domain.Model.Warehouses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.EntityFramework.Configuration;
using Domain.Model.UserRoles;

namespace Persistence.EntityFramework;

public sealed class AppDbContext : DbContext, IUnitOfWork
{
    public DbSet<BudgetLine> BudgetLines { get; private set; }
    public DbSet<Currency> Currencies { get; private set; }
    public DbSet<Department> Departments { get; private set; }
    public DbSet<EntityChange> EntityChanges { get; private set; }
    public DbSet<Good> Goods { get; private set; }
    public DbSet<Measure> Measures { get; private set; }
    public DbSet<Permission> Permissions { get; private set; }
    public DbSet<Requisition> Requisitions { get; private set; }
    public DbSet<RequisitionLine> RequisitionLines { get; private set; }
    public DbSet<Role> Roles { get; private set; }
    public DbSet<Supplier> Suppliers { get; private set; }
    public DbSet<User> Users { get; private set; }
    public DbSet<UserRole> UserRoles { get; private set; }
    public DbSet<Warehouse> Warehouses { get; private set; }

    private readonly PersistenceSettings _settings;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public AppDbContext(DbContextOptions<AppDbContext> options, PersistenceSettings settings, IServiceScopeFactory serviceScopeFactory) : base(options)
    {
        _settings = settings;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        if (_settings.LogEntityChanges)
            await LogEntityChanges(cancellationToken);

        return await base.SaveChangesAsync(cancellationToken);
    }

    private async Task LogEntityChanges(CancellationToken cancellationToken = default)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var currentUserService = scope.ServiceProvider.GetRequiredService<ICurrentUserService>();

            var performerId = currentUserService.Details?.Id;

            var modifyEntries = ChangeTracker.Entries().Where(e => e.Entity is IAuditableEntity && e.State == EntityState.Modified);

            foreach (var modifyEntry in modifyEntries)
            {
                ((IAuditableEntity)modifyEntry.Entity).RefreshUpdatedAt();

                var idProperty = modifyEntry.Properties.FirstOrDefault(p => p.Metadata.IsPrimaryKey());
                if (idProperty == null)
                    continue;

                var entityName = modifyEntry.Entity.GetType().Name;
                var entityId = idProperty?.OriginalValue?.ToString() ?? string.Empty;

                foreach (var modifiedProperty in modifyEntry.Properties.Where(p => p.IsModified))
                {
                    var propertyName = modifiedProperty.Metadata.GetColumnName();
                    if (propertyName == nameof(IAuditableEntity.UpdatedAt))
                        continue;

                    var oldValue = modifiedProperty.OriginalValue?.ToString();
                    var newValue = modifiedProperty.CurrentValue?.ToString();

                    var entityChange = new EntityChange(AppGuid.New, entityName, entityId, propertyName, oldValue, newValue, performerId);

                    await EntityChanges.AddAsync(entityChange, cancellationToken);
                }
            }
        }
    }
}