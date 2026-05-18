using CleanArchitectureLearnProject.Domain.Abstractions;
using CleanArchitectureLearnProject.Domain.Employees;
using GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureLearnProject.Infrastructure.Application;

internal sealed class ApplicationDbContext : DbContext,IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<Entity>();
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(p => p.CreateAt)
                               .CurrentValue = DateTimeOffset.Now;
            }
            if (entry.State == EntityState.Modified)
            {
                if (entry.Property(p => p.IsDelete).CurrentValue == true)
                {
                    entry.Property(p => p.DeleteAt)
                              .CurrentValue = DateTimeOffset.Now;
                }
                else
                {
                    entry.Property(p => p.UpdateAt)
                            .CurrentValue = DateTimeOffset.Now;
                }
               
            }
            if (entry.State == EntityState.Deleted)
            {
                throw new ArgumentException("Db!den direkt silme işlemi yapamazsınız");
            }
           
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
