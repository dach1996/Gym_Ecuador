namespace Common.Cache.Model;

/// <summary>
/// Ajustes de Cache
/// </summary>
public class CacheConfigurationModel
{
    /// <summary>
    /// Duración por Default de Cache
    /// </summary>
    /// <value></value>
    public int DefaultDurationSeconds { get; set; }

    /// <summary>
    /// Ajustes
    /// </summary>
    /// <value></value>
    public Dictionary<string, ConfigurationBase> ConfigurationByKey { get; set; }

    /// <summary>
    /// Ajustes
    /// </summary>
    /// <value></value>
    public Dictionary<string, ConfigurationBase> ConfigurationByRegularExpression { get; set; }
}

/// <summary>
/// Ajuste
/// </summary>
public class ConfigurationBase
{
    /// <summary>
    /// Duración en minutos del cache
    /// </summary>
    /// <value></value>
    public int DurationSeconds { get; set; }
}
