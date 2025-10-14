using Microsoft.Extensions.Logging;

namespace Common.WebApi.Models;
/// <summary>
/// Log
/// </summary>
public class LogModel
{
    /// <summary>
    /// Mensaje a guardar en el log
    /// </summary>
    public string LogMessage { get; set; } = string.Empty;

    /// <summary>
    /// Tipo de Log : Request o Response
    /// </summary>
    public string TypeLog { get; set; } = string.Empty;

    /// <summary>
    /// Mensaje a guardar en el log
    /// </summary>
    public string Method { get; set; } = string.Empty;

    /// <summary>
    /// Mensaje a guardar en el log
    /// </summary>
    public string Path { get; set; } = string.Empty;

    /// <summary>
    /// Modelo a guardar en el log
    /// </summary>
    public object Body { get; set; }

    /// <summary>
    /// Modelo a guardar en el log
    /// </summary>
    public object Header { get; set; }

    /// <summary>
    /// Nivel de Log
    /// </summary>
    public LogLevel LogLevel { get; set; } = LogLevel.Information;
}

