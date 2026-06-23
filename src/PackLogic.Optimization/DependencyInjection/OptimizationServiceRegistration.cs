using Microsoft.Extensions.DependencyInjection;

namespace PackLogic.Optimization.DependencyInjection;

/// <summary>
/// Registers services that belong to the optimization layer.
/// </summary>
public static class OptimizationServiceRegistration
{
    /// <summary>
    /// Adds optimization-layer services to the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection used by the API startup pipeline.</param>
    /// <returns>The same service collection so registrations can be chained.</returns>
    public static IServiceCollection AddOptimizationServices(this IServiceCollection services)
    {
        // I keep recommendation and packing algorithm registrations here so the
        // API can depend on the optimization layer without knowing how each
        // algorithm is built internally.
        //
        // Future example:
        // services.AddScoped<IPackagingOptimizer, PackagingOptimizer>();

        return services;
    }
}