using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Essentials.Configuration;

/// <summary>
/// Автоматический конфигуратор сервиса
/// </summary>
public interface IAutoServiceConfigurator
{
    /// <summary>
    /// Настраивает сервис
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    void Configure(IServiceCollection services, IConfiguration configuration);
}