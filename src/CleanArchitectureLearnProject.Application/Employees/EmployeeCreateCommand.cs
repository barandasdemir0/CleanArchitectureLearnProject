using CleanArchitectureLearnProject.Domain.Employees;
using FluentValidation;
using GenericRepository;
using Mapster;
using MediatR;
using TS.Result;

namespace CleanArchitectureLearnProject.Application.Employees;

public sealed class EmployeeCreateCommandValidator : AbstractValidator<EmployeeCreateCommand>
{
    public EmployeeCreateCommandValidator()
    {
        RuleFor(x => x.FirstName).MinimumLength(3).WithMessage("Ad alanı en az 3 karakter olmalıdır");
        RuleFor(x => x.LastName).MinimumLength(3).WithMessage("SoyAd alanı en az 3 karakter olmalıdır");
        RuleFor(x => x.PersonelInformation.TCNo).MinimumLength(11).WithMessage("TCNo alanı 11 karakter olmalıdır").MaximumLength(11).WithMessage("TCNo alanı 11 karakter olmalıdır");
    }
}


public sealed record EmployeeCreateCommand(
    string FirstName,
    string LastName,
    DateOnly BirthOfDate,
    decimal Salary,
    PersonelInformation PersonelInformation,
    Address? Address
    ):IRequest<Result<string>>;

internal sealed class EmployeeCreateCommandHandler(IEmployeeRepository employeeRepository,IUnitOfWork unitOfWork) : IRequestHandler<EmployeeCreateCommand, Result<string>>
{
    public async Task<Result<string>> Handle(EmployeeCreateCommand request, CancellationToken cancellationToken)
    {

        //uniq kontrol yaptık
        var isEmployeeExists = await employeeRepository.AnyAsync(x => x.PersonelInformation.TCNo == request.PersonelInformation.TCNo);
        if (isEmployeeExists)
        {
            return Result<string>.Failure("Bu TC numarası daha önce kaydedilmiş");
        }
        Employee employee = request.Adapt<Employee>();
        employeeRepository.Add(employee); //add işlemi async olunca performans kaydı oluyor bu sebeple
        await unitOfWork.SaveChangesAsync(cancellationToken); //kaydetme işlemi async ama add işlemi normal
        return "Personel Kaydı Başarıyla tamamlandı";

    }
}
