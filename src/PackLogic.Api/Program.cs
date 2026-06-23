using PackLogic.Application.DependencyInjection;
using PackLogic.Infrastructure.DependencyInjection;
using PackLogic.Optimization.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// I register Swagger services so I can inspect and test API endpoints
// quickly while developing the backend locally.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// I registered each project layer through its own extension method.
// This keeps Program.cs small and protects the clean architecture structure.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddOptimizationServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "PackLogic API v1");
        options.RoutePrefix = "swagger";
    });
}

// I am leaving HTTPS redirection disabled for now because the local API is
// currently running on HTTP only. I will re-enable it when HTTPS launch
// settings are configured properly.
// app.UseHttpsRedirection();

app.MapGet("/api/health", () =>
{
    return Results.Ok(new
    {
        status = "Healthy",
        application = "PackLogic"
    });
})
.WithName("HealthCheck");

app.Run();