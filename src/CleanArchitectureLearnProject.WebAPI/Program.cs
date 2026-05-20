using CleanArchitectureLearnProject.Application;
using CleanArchitectureLearnProject.Infrastructure;
using CleanArchitectureLearnProject.WebAPI;
using CleanArchitectureLearnProject.WebAPI.Controllers;
using CleanArchitectureLearnProject.WebAPI.Modules;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.RateLimiting;
using Scalar.AspNetCore;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddApplication();
builder.Services.AddInfrasturecture(builder.Configuration);
builder.Services.AddCors();
builder.Services.AddOpenApi();
builder.Services.AddControllers().AddOData(opt=> 
opt
.Select()
.Filter()
.Count()
.Expand()
.OrderBy()
.SetMaxTop(null)
.AddRouteComponents("odata",AppODataController.GetEdmModel())
);
builder.Services.AddRateLimiter(x => x.AddFixedWindowLimiter("fixed", cfg =>
{
    cfg.QueueLimit = 100;
    cfg.Window = TimeSpan.FromSeconds(1);
    cfg.PermitLimit = 100;
    cfg.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
}));

builder.Services.AddExceptionHandler<ExceptionHandler>().AddProblemDetails();

var app = builder.Build();



app.MapOpenApi();
app.MapScalarApiReference();
app.MapDefaultEndpoints();
app.UseCors(x => x.AllowAnyHeader().AllowCredentials().AllowAnyMethod().SetIsOriginAllowed(t => true));
//signal R ile çalışınılacaksa allowcredintials kullanılması gerekir ve setis origin allow
app.RegisterRoutes();
app.UseExceptionHandler();
app.MapControllers().RequireRateLimiting("fixed");

ExtensionsMiddleware.CreateFirstUser(app);
app.Run();
