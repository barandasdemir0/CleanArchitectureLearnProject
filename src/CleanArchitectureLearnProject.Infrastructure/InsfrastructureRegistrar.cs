using CleanArchitectureLearnProject.Application.Services;
using CleanArchitectureLearnProject.Domain.Users;
using CleanArchitectureLearnProject.Infrastructure.Application;
using CleanArchitectureLearnProject.Infrastructure.Options;
using CleanArchitectureLearnProject.Infrastructure.Services;
using GenericRepository;
using Keycloak.AuthServices.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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

        services.AddIdentity<AppUser, IdentityRole<Guid>>(opt =>
        {
            opt.Password.RequiredLength = 1;
            opt.Password.RequireNonAlphanumeric = false;
            opt.Password.RequireDigit = false;
            opt.Password.RequireLowercase = false;
            opt.Password.RequireUppercase = false;
            opt.Lockout.MaxFailedAccessAttempts = 5;
            opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            opt.SignIn.RequireConfirmedEmail = true;
        })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();


        services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
        services.ConfigureOptions<JwtOptionsSetup>();
        services.Configure<KeycloakConfiguration>(configuration.GetSection("KeycloakConfiguration"));
        services.AddScoped<KeycloakService>();

        services.AddScoped<IJwtProvider, KeycloakService>();

        //keycloacke geçildiği için bu kapandı
        //services.AddAuthentication(opt =>
        //{
        //    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //}).AddJwtBearer();
       

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
