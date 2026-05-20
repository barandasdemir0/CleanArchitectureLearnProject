using CleanArchitectureLearnProject.Application.Auth;
using MediatR;
using TS.Result;

namespace CleanArchitectureLearnProject.WebAPI.Modules;

public static class AuthModule
{
    public static void RegisterAuthRoutes(this IEndpointRouteBuilder endpoint)
    {
        RouteGroupBuilder groupBuilder = endpoint.MapGroup("/auth").WithTags("Auth");
        groupBuilder.MapPost("login",
            async (ISender sender, LoginCommand request, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(request, cancellationToken);
                return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
            }).Produces<Result<LoginCommandResponse>>();
    }
}
