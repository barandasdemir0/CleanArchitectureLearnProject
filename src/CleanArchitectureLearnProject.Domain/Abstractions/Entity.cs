namespace CleanArchitectureLearnProject.Domain.Abstractions;

//audit log
public abstract class Entity
{
    public Entity()
    {
        Id = Guid.CreateVersion7();
    }
    public Guid Id { get; set; }
    #region audit log
    public DateTimeOffset CreateAt { get; set; }
    public Guid CreateUserId { get; set; } = default!; //create işlemi yapan userın ıdsı zorunlu hale getirdik
    public DateTimeOffset? UpdateAt { get; set; }
    public Guid? UpdateUserId { get; set; } 
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeleteAt { get; set; }
    public Guid? DeleteUserId { get; set; }
    #endregion
}
