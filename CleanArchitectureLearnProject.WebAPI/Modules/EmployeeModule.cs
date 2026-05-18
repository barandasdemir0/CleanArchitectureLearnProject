using CleanArchitectureLearnProject.Application.Employees;
using MediatR;
using TS.Result;

namespace CleanArchitectureLearnProject.WebAPI.Modules;

public static class EmployeeModule
{
    public static void RegisterEmployeeRoutes(this IEndpointRouteBuilder endpoint)
    {
        RouteGroupBuilder groupBuilder = endpoint.MapGroup("/employees").WithTags("employees");
        groupBuilder.MapPost(string.Empty,
            async (ISender sender, EmployeeCreateCommand request, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(request, cancellationToken);
                return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
            }).Produces<Result<string>>();
    }
}
