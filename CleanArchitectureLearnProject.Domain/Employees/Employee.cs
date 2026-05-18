using CleanArchitectureLearnProject.Domain.Abstractions;

namespace CleanArchitectureLearnProject.Domain.Employees;

public sealed class Employee:Entity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => string.Join(" ", FirstName, LastName);
    public DateOnly BirthOfDate { get; set; }
    public decimal Salary { get; set; }
    public PersonelInformation? PersonelInformation { get; set; }
    public Address? Address { get; set; }
}