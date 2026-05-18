var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.CleanArchitectureLearnProject_WebAPI>("cleanarchitecturelearnproject-webapi");

builder.Build().Run();
