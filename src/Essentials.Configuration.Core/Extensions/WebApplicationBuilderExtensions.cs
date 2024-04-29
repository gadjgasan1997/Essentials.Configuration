using NLog.Extensions.Logging;
using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static Essentials.Configuration.Dictionaries.KnownLocations;

namespace Essentials.Configuration.Extensions;

/// <summary>
/// Методы расширения для <see cref="WebApplicationBuilder" />
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Настраивает приложение по-умолчанию
    /// </summary>
    /// <param name="builder">Билдер приложения</param>
    /// <param name="configureServicesAction">Делегат регистрации сервисов</param>
    /// <param name="initConfigurationAction">Делегат инициализации конфигурации</param>
    /// <param name="postConfigurationAction">Делегат дополнительной настройки конфигурации</param>
    /// <param name="loggingConfigurationAction">Делегат конфигурации логирования</param>
    /// <param name="metricsConfigurationAction">Делегат конфигурации метрик</param>
    /// <returns></returns>
    public static WebApplicationBuilder ConfigureDefault(
        this WebApplicationBuilder builder,
        Action<HostBuilderContext, IServiceCollection>? configureServicesAction = null,
        Action<ConfigurationManager>? initConfigurationAction = null,
        Action<ConfigurationManager>? postConfigurationAction = null,
        Action<ILoggingBuilder>? loggingConfigurationAction = null,
        Action<ConfigurationManager>? metricsConfigurationAction = null)
    {
        // Прописывание делегатов по-умолчанию
        configureServicesAction ??= (_, _) => { };
        
        initConfigurationAction ??= manager =>
        {
            manager
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(Path.Combine(SETTINGS_PATH, "appsettings.json"))
                .AddJsonFile(Path.Combine(SETTINGS_PATH, $"appsettings.{builder.Environment.EnvironmentName}.json"), true)
                .AddEnvironmentVariables();
        };
        
        loggingConfigurationAction ??= loggingBuilder => loggingBuilder.ClearProviders().AddNLog();
        metricsConfigurationAction ??= _ => builder.Host.AddMetrics();

        // Вызов делегатов конфигурации
        initConfigurationAction(builder.Configuration);
        postConfigurationAction?.Invoke(builder.Configuration);
        
        loggingConfigurationAction(builder.Logging);
        metricsConfigurationAction(builder.Configuration);
        
        builder.Host.ConfigureServices(configureServicesAction);

        return builder;
    }

    private static void AddMetrics(this IHostBuilder builder)
    {
        builder.UseMetrics(options =>
        {
            options.EndpointOptions = endpointsOptions =>
            {
                endpointsOptions.MetricsEndpointOutputFormatter = new MetricsPrometheusTextOutputFormatter();
            };
        });
    }
}