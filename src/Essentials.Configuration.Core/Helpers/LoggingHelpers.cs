using static Essentials.Configuration.Dictionaries.KnownLocations;

namespace Essentials.Configuration.Helpers;

/// <summary>
/// Хелперы логирования
/// </summary>
public static class LoggingHelpers
{
    /// <summary>
    /// Возвращает путь до конфига лоигрования
    /// </summary>
    /// <param name="environment">Название среды</param>
    /// <returns></returns>
    public static string GetNLogConfigPath(string environment)
    {
        var environmentConfigPath = Path.Combine(SETTINGS_PATH, $"nlog.{environment}.config");
        return File.Exists(environmentConfigPath)
            ? environmentConfigPath
            : Path.Combine(SETTINGS_PATH, "nlog.config");
    }
}