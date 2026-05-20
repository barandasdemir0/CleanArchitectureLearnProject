namespace CleanArchitectureLearnProject.WebAPI.Modules;

public static class RouteRegistrar
{
    public static void RegisterRoutes(this IEndpointRouteBuilder app)
    {
        app.RegisterEmployeeRoutes();
        app.RegisterAuthRoutes();
    }
}
