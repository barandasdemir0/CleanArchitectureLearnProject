using CleanArchitectureLearnProject.Application.Services;
using CleanArchitectureLearnProject.Domain.Users;
using CleanArchitectureLearnProject.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchitectureLearnProject.Infrastructure.Services;

internal sealed class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
{
    public Task<string> CreateTokenAsync(AppUser user, CancellationToken cancellationToken)
    {
        List<Claim> claims = new()
        {
            new Claim("user-id",user.Id.ToString())
        };

        var expires = DateTime.Now.AddDays(1);

        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(options.Value.SecretKey));
        SigningCredentials signingCredentials = new(securityKey,SecurityAlgorithms.HmacSha512);

        JwtSecurityToken securityToken = new(
            issuer:options.Value.Issuer,
            audience: options.Value.Audience,
            claims:claims,
            notBefore: DateTime.Now,
            expires:expires,
            signingCredentials:signingCredentials
            );

        JwtSecurityTokenHandler handler = new();

        string token = handler.WriteToken(securityToken);

        return Task.FromResult(token);
    }
}
