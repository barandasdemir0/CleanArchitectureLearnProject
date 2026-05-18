namespace CleanArchitectureLearnProject.Domain.Abstractions;

public abstract class EntityDto
{
    public Guid Id { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime? UpdateAt { get; set; }
    public bool IsDelete { get; set; }
    public DateTime? DeleteAt { get; set; }
}
