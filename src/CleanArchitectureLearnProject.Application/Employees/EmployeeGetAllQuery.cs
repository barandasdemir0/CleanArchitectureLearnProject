using CleanArchitectureLearnProject.Domain.Abstractions;
using CleanArchitectureLearnProject.Domain.Employees;
using CleanArchitectureLearnProject.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitectureLearnProject.Application.Employees;

public sealed record EmployeeGetAllQuery() : IRequest<IQueryable<EmployeeGetAllQueryResponse>>;

public sealed class EmployeeGetAllQueryResponse : EntityDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateOnly BirthOfDate { get; set; }
    public decimal Salary { get; set; }
    public string TCNo { get; set; } = default!;
}

internal sealed class EmployeeGetAllQueryHandler(IEmployeeRepository employeeRepository, UserManager<AppUser> userManager) : IRequestHandler<EmployeeGetAllQuery, IQueryable<EmployeeGetAllQueryResponse>>
{
    public Task<IQueryable<EmployeeGetAllQueryResponse>> Handle(EmployeeGetAllQuery request, CancellationToken cancellationToken)
    {
        var response = (from Employee
                        in employeeRepository.GetAll()

                        join create_user in userManager.Users.AsQueryable()
                        on Employee.CreateUserId equals create_user.Id

                        join update_user in userManager.Users.AsQueryable()
                        on Employee.CreateUserId equals update_user.Id into update_user
                        from update_users in update_user.DefaultIfEmpty()
                        select new EmployeeGetAllQueryResponse
                        {
                            FirstName = Employee.FirstName,
                            LastName = Employee.LastName,
                            Salary = Employee.Salary,
                            BirthOfDate = Employee.BirthOfDate,
                            CreateAt = Employee.CreateAt,
                            UpdateAt = Employee.UpdateAt,
                            DeleteAt = Employee.DeleteAt,
                            Id = Employee.Id,
                            IsDelete = Employee.IsDeleted,
                            TCNo = Employee.PersonelInformation.TCNo,
                            CreateUserId = Employee.CreateUserId,
                            CreateUserName = create_user.FullName + "(" + create_user.Email + ")",
                            UpdateUserId = Employee.UpdateUserId,
                            UpdateUserName = Employee.UpdateUserId == null ? null : update_users.FullName + "(" + update_users.Email + ")"
                        }).AsQueryable();


        return Task.FromResult(response);
    }
}