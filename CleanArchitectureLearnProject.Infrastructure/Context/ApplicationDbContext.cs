using CleanArchitectureLearnProject.Domain.Employees;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureLearnProject.Infrastructure.Application;

internal sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Employee> Employees { get; set; }
}
