using Application.Contracts.Infra.Persistence;
using Domain.Model.Suppliers;
using Domain.Model.Users;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EntityFramework;

public sealed class AppDbContext : DbContext, IUnitOfWork
{
    public DbSet<Supplier> Suppliers { get; private set; }
    public DbSet<User> Users { get; private set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}