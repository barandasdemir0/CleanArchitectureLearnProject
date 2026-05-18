namespace CleanArchitectureLearnProject.Domain.Abstractions;

public abstract class Entity
{
    public Entity()
    {
        Id = Guid.CreateVersion7();
    }
    public Guid Id { get; set; }
    public DateTimeOffset CreateAt { get; set; }
    public DateTimeOffset? UpdateAt { get; set; }
    public bool IsDelete { get; set; }
    public DateTimeOffset? DeleteAt { get; set; }
}
