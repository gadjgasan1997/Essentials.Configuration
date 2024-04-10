using Microsoft.Extensions.DependencyInjection;

namespace Essentials.Configuration.Extensions;

/// <summary>
/// Методы расширения для <see cref="IServiceCollection" />, необходимые для регистрации сервисов
/// </summary>
public static class RegisterServicesExtensions
{
    /// <summary>
    /// Настраивает сервис с использованием зарегистрированного ранее сервиса
    /// </summary>
    /// <param name="services">Список сервисов</param>
    /// <param name="configureServiceAction">Действие по конфигурации нового сервиса</param>
    /// <typeparam name="TRegisteredService">Зарегистрированный ранее сервис</typeparam>
    /// <returns>Список сервисов</returns>
    public static IServiceCollection ConfigureWithRegisteredService<TRegisteredService>(
        this IServiceCollection services,
        Action<TRegisteredService> configureServiceAction)
        where TRegisteredService : notnull
    {
        var provider = services.BuildServiceProvider();
        
        var service = provider.GetRequiredService<TRegisteredService>();
        
        configureServiceAction(service);

        return services;
    }
    
    /// <summary>
    /// Настраивает сервис с использованием зарегистрированных ранее сервисов
    /// </summary>
    /// <param name="services">Список сервисов</param>
    /// <param name="configureServiceAction">Действие по конфигурации нового сервиса</param>
    /// <typeparam name="TRegisteredService">Зарегистрированный ранее сервис</typeparam>
    /// <typeparam name="TRegisteredService2">Зарегистрированный ранее сервис</typeparam>
    /// <returns>Список сервисов</returns>
    public static IServiceCollection ConfigureWithRegisteredService<TRegisteredService, TRegisteredService2>(
        this IServiceCollection services,
        Action<TRegisteredService, TRegisteredService2> configureServiceAction)
        where TRegisteredService : notnull
        where TRegisteredService2 : notnull
    {
        var provider = services.BuildServiceProvider();
        
        var service = provider.GetRequiredService<TRegisteredService>();
        var service2 = provider.GetRequiredService<TRegisteredService2>();
        
        configureServiceAction(service, service2);

        return services;
    }
    
    /// <summary>
    /// Настраивает сервис с использованием зарегистрированных ранее сервисов
    /// </summary>
    /// <param name="services">Список сервисов</param>
    /// <param name="configureServiceAction">Действие по конфигурации нового сервиса</param>
    /// <typeparam name="TRegisteredService">Зарегистрированный ранее сервис</typeparam>
    /// <typeparam name="TRegisteredService2">Зарегистрированный ранее сервис</typeparam>
    /// <typeparam name="TRegisteredService3">Зарегистрированный ранее сервис</typeparam>
    /// <returns>Список сервисов</returns>
    public static IServiceCollection ConfigureWithRegisteredService<TRegisteredService, TRegisteredService2, TRegisteredService3>(
        this IServiceCollection services,
        Action<TRegisteredService, TRegisteredService2, TRegisteredService3> configureServiceAction)
        where TRegisteredService : notnull
        where TRegisteredService2 : notnull
        where TRegisteredService3 : notnull
    {
        var provider = services.BuildServiceProvider();
        
        var service = provider.GetRequiredService<TRegisteredService>();
        var service2 = provider.GetRequiredService<TRegisteredService2>();
        var service3 = provider.GetRequiredService<TRegisteredService3>();
        
        configureServiceAction(service, service2, service3);

        return services;
    }
    
    /// <summary>
    /// Настраивает сервис с использованием зарегистрированных ранее сервисов
    /// </summary>
    /// <param name="services">Список сервисов</param>
    /// <param name="configureServiceAction">Действие по конфигурации нового сервиса</param>
    /// <param name="registeredServicesTypes">Типы зарегистрированных ранее сервисов</param>
    /// <returns>Список сервисов</returns>
    public static IServiceCollection ConfigureWithRegisteredServices(
        this IServiceCollection services,
        Action<List<object>> configureServiceAction,
        params Type[] registeredServicesTypes)
    {
        return services.ConfigureWithRegisteredServices(registeredServicesTypes, configureServiceAction);
    }
    
    /// <summary>
    /// Настраивает сервис с использованием зарегистрированных ранее сервисов
    /// </summary>
    /// <param name="services">Список сервисов</param>
    /// <param name="registeredServicesTypes">Типы зарегистрированных ранее сервисов</param>
    /// <param name="configureServiceAction">Действие по конфигурации нового сервиса</param>
    /// <returns>Список сервисов</returns>
    public static IServiceCollection ConfigureWithRegisteredServices(
        this IServiceCollection services,
        IEnumerable<Type> registeredServicesTypes,
        Action<List<object>> configureServiceAction)
    {
        var provider = services.BuildServiceProvider();

        var registeredServices = registeredServicesTypes.Select(provider.GetRequiredService).ToList();
        
        configureServiceAction(registeredServices);

        return services;
    }
}