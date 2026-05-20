using CleanArchitectureLearnProject.Domain.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureLearnProject.Domain.Abstractions;

//audit log
public abstract class Entity
{
    public Entity()
    {
        Id = Guid.CreateVersion7();
    }
    public Guid Id { get; set; }
    public bool IsActive { get; set; } = true;
    #region audit log
    public DateTimeOffset CreateAt { get; set; }
    public string CreateUserName => GetCreateUserName();
    public Guid CreateUserId { get; set; } = default!; //create işlemi yapan userın ıdsı zorunlu hale getirdik
    public DateTimeOffset? UpdateAt { get; set; }
    public Guid? UpdateUserId { get; set; }
    public string UpdateUserName => GetUpdateUserName();
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeleteAt { get; set; }
    public Guid? DeleteUserId { get; set; }
    #endregion
    #region audit method
    private  string GetCreateUserName()
    {
        HttpContextAccessor httpContextAccessor = new();
        var userManager = httpContextAccessor.HttpContext.RequestServices.GetRequiredService<UserManager<AppUser>>();
        AppUser appUser = userManager.Users.First(x => x.Id == CreateUserId);

        return appUser.FullName + "(" + appUser.Email + ")";
    }

    private string GetUpdateUserName()
    {
        if (UpdateUserId is null)
        {
            return string.Empty;
        }

        HttpContextAccessor httpContextAccessor = new();
        var userManager = httpContextAccessor.HttpContext.RequestServices.GetRequiredService<UserManager<AppUser>>();
        AppUser appUser = userManager.Users.First(x => x.Id == UpdateUserId);

        return appUser.FullName + "(" + appUser.Email + ")";
    }
    #endregion
}
