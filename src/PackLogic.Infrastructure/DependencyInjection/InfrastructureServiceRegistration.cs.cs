using Microsoft.Extensions.DependencyInjection;

namespace PackLogic.Infrastructure.DependencyInjection;

/// <summary>
/// Registers services that belong to the infrastructure layer.
/// </summary>
public static class InfrastructureServiceRegistration
{
    /// <summary>
    /// Adds infrastructure-layer services to the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection used by the API startup pipeline.</param>
    /// <returns>The same service collection so registrations can be chained.</returns>
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        // I keep database, persistence, and external system registrations here.
        // This keeps infrastructure details out of Program.cs and protects the
        // clean architecture boundary.
        //
        // Future example:
        // services.AddDbContext<PackLogicDbContext>(...);

        return services;
    }
}