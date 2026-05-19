using CleanArchitectureLearnProject.Domain.Users;
using CleanArchitectureLearnProject.Infrastructure.Application;
using GenericRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace CleanArchitectureLearnProject.Infrastructure;

public static class InsfrastructureRegistrar
{
    public static IServiceCollection AddInfrasturecture(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(opt =>
        {
            string connection = configuration.GetConnectionString("SqlServer")!;
            opt.UseSqlServer(connection);
        });

        services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<ApplicationDbContext>());

        services.AddIdentity<AppUser, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.Scan(opt =>
        {
            opt.FromAssemblies(typeof(InsfrastructureRegistrar).Assembly) //mevcut katmanımı ara 
            .AddClasses(publicOnly: false)//classı public olmayanlar dahil hepsini listele 
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()//interface ile implemente olanları getir
            .WithScopedLifetime();//scop yaşam döngüsünde yap
        });

        return services;
    }
}
