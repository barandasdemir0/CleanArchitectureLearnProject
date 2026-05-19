using CleanArchitectureLearnProject.Domain.Abstractions;
using CleanArchitectureLearnProject.Domain.Employees;
using MediatR;

namespace CleanArchitectureLearnProject.Application.Employees;

public sealed record EmployeeGetAllQuery() : IRequest<IQueryable<EmployeeGetAllQueryResponse>>;

public sealed class EmployeeGetAllQueryResponse: EntityDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateOnly BirthOfDate { get; set; }
    public decimal Salary { get; set; }
    public string TCNo { get; set; } = default!;
}

internal sealed class EmployeeGetAllQueryHandler(IEmployeeRepository employeeRepository) : IRequestHandler<EmployeeGetAllQuery, IQueryable<EmployeeGetAllQueryResponse>>
{
    public Task<IQueryable<EmployeeGetAllQueryResponse>> Handle(EmployeeGetAllQuery request, CancellationToken cancellationToken)
    {
        var response = employeeRepository.GetAll().Select(s=> new EmployeeGetAllQueryResponse
        {
            FirstName =s.FirstName,
            LastName =s.LastName,
            Salary =s.Salary,
            BirthOfDate =s.BirthOfDate,
            CreateAt = s.CreateAt,
            UpdateAt = s.UpdateAt,
            DeleteAt = s.DeleteAt,
            Id = s.Id,
            IsDelete = s.IsDeleted,
            TCNo = s.PersonelInformation.TCNo,
        }).AsQueryable();

        return Task.FromResult(response);
    }
}