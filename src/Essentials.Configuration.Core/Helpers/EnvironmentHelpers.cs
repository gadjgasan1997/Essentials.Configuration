using Essentials.Utils.Extensions;
using static System.Environment;
using static Essentials.Configuration.Dictionaries.CommonEnvironmentVariables;

namespace Essentials.Configuration.Helpers;

/// <summary>
/// Хелперы для работы с окружением
/// </summary>
public static class EnvironmentHelpers
{
    /// <summary>
    /// Возвращает название приложения
    /// </summary>
    /// <returns></returns>
    public static string GetApplicationName() =>
        GetEnvironmentVariable(APPLICATION_NAME)
            .CheckNotNullOrEmpty("Не указано название приложения", APPLICATION_NAME);
}