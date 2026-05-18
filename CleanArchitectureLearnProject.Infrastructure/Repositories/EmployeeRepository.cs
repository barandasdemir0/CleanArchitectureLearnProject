using CleanArchitectureLearnProject.Domain.Employees;
using CleanArchitectureLearnProject.Infrastructure.Application;
using GenericRepository;

namespace CleanArchitectureLearnProject.Infrastructure.Repositories;

internal class EmployeeRepository : Repository<Employee, ApplicationDbContext>, IEmployeeRepository
{
    public EmployeeRepository(ApplicationDbContext context) : base(context)
    {
    }
}
