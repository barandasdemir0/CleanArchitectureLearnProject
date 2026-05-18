namespace CleanArchitectureLearnProject.Domain.Employees;

public sealed record Address
{
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Town { get; set; } = string.Empty;
    public string FullAddress { get; set; } = string.Empty;
}
    



