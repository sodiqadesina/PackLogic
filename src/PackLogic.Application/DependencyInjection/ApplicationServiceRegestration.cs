using Microsoft.Extensions.DependencyInjection;

namespace PackLogic.Application.DependencyInjection;

/// <summary>
/// Registers services that belong to the application layer.
/// </summary>
public static class ApplicationServiceRegistration
{
    /// <summary>
    /// Adds application-layer services to the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection used by the API startup pipeline.</param>
    /// <returns>The same service collection so registrations can be chained.</returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // I keep application-layer registrations here so Program.cs stays clean
        // and the API does not need to know every service implementation directly.
        //
        // Future examples:
        // services.AddScoped<IPartService, PartService>();
        // services.AddScoped<IBagService, BagService>();
        // services.AddScoped<IBoxService, BoxService>();

        return services;
    }
}