using CleanArchitectureLearnProject.Domain.Employees;
using MediatR;
using TS.Result;

namespace CleanArchitectureLearnProject.Application.Employees;

public sealed record EmployeeGetQuery
(
    Guid Id
) : IRequest<Result<Employee>>;


internal sealed class EmployeeGetQueryHandler(IEmployeeRepository employeeRepository) : IRequestHandler<EmployeeGetQuery, Result<Employee>>
{
    public async Task<Result<Employee>> Handle(EmployeeGetQuery request, CancellationToken cancellationToken)
    {
        var employee = await employeeRepository.FirstOrDefaultAsync(p => p.Id == request.Id,cancellationToken);
        if (employee is null)
        {
            return Result<Employee>.Failure("Personel Bulunamadı");
        }

        return employee;
    }
}