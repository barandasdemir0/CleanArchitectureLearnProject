using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureLearnProject.Domain.Abstractions;

public abstract class Entity
{
    public Entity()
    {
        Id = Guid.CreateVersion7();
    }
    public Guid Id { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime? UpdateAt { get; set; }
    public bool IsDelete { get; set; }
    public DateTime? DeleteAt { get; set; }
}
