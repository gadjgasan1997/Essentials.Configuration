using Microsoft.Extensions.DependencyInjection;

namespace Essentials.Configuration.Extensions;

/// <summary>
/// Методы расширения для <see cref="IServiceCollection" />
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Атомарно настраивает сервисы
    /// </summary>
    /// <param name="services"></param>
    /// <param name="isConfigured"></param>
    /// <param name="configureAction"></param>
    /// <returns></returns>
    public static IServiceCollection AtomicConfigureService(
        this IServiceCollection services,
        ref uint isConfigured,
        Action configureAction)
    {
        if (Interlocked.Exchange(ref isConfigured, 1) == 1)
            return services;

        configureAction();
        return services;
    }
}