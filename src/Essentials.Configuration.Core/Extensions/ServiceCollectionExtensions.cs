using System.Reflection;
using Essentials.Utils.Collections.Extensions;
using Essentials.Utils.Reflection.Extensions;
using Microsoft.Extensions.Configuration;
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
    
    /// <summary>
    /// Вызывает авто кофнигурацию сервисов
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="assemblies"></param>
    /// <returns></returns>
    public static IServiceCollection InvokeAutoServiceConfigurators(
        this IServiceCollection services,
        IConfiguration configuration,
        List<Assembly>? assemblies = null)
    {
        assemblies ??= [Assembly.GetEntryAssembly()!];
        
        var configurators = assemblies
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.GetTypeInfo().IsImplements<IAutoServiceConfigurator>())
            .Select(Activator.CreateInstance)
            .Cast<IAutoServiceConfigurator>();

        configurators.ForEach(configurator => configurator.Configure(services, configuration));
        
        return services;
    }
}