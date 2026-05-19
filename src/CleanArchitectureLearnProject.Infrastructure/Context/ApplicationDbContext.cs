using CleanArchitectureLearnProject.Domain.Abstractions;
using CleanArchitectureLearnProject.Domain.Employees;
using CleanArchitectureLearnProject.Domain.Users;
using GenericRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureLearnProject.Infrastructure.Application;

internal sealed class ApplicationDbContext : IdentityDbContext<AppUser,IdentityRole<Guid>,Guid>,IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);//böylelikle hiç birini ignore etmiyoruz hepsini oluşturuyoruz
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        //modelBuilder.Ignore<IdentityUserClaim<Guid>>();
        //modelBuilder.Ignore<IdentityRoleClaim<Guid>>();
        //modelBuilder.Ignore<IdentityUserToken<Guid>>();
        //modelBuilder.Ignore<IdentityUserLogin<Guid>>();
        //modelBuilder.Ignore<IdentityUserRole<Guid>>();
        //bunlarıda kaldırıyoruz ama bunun yapmak yerine çoğunu alalım mantıklı olur
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
                if (entry.Property(p => p.IsDeleted).CurrentValue == true)
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
                throw new ArgumentException("Db'den direkt silme işlemi yapamazsınız");
            }
           
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
