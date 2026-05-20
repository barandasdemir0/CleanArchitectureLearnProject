using CleanArchitectureLearnProject.Domain.Users;

namespace CleanArchitectureLearnProject.Application.Services;

public interface IJwtProvider
{
    public Task<string> CreateTokenAsync(AppUser user, string password, CancellationToken cancellationToken);
}
