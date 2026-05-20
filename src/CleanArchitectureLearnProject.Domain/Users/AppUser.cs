using Microsoft.AspNetCore.Identity;

namespace CleanArchitectureLearnProject.Domain.Users;

public sealed class AppUser:IdentityUser<Guid>
{
    public AppUser()
    {
        Id = Guid.CreateVersion7();
    }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string FullName => $"{FirstName} {LastName}"; //computed property

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
