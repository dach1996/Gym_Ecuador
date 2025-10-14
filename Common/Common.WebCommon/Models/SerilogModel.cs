using Serilog;
using Serilog.Events;

namespace Common.WebCommon.Models;
/// <summary>
/// Modelo para Serilog
/// </summary>
public class SerilogModel
{
    /// <summary>
    /// Niveles Mónimos
    /// </summary>
    public MinimumLevel MinimumLevel { get; set; }

    /// <summary>
    /// Escribir en
    /// </summary>
    public List<WriteTo> WriteTo { get; set; }
}

/// <summary>
/// Mínimo Level
/// </summary>
public class MinimumLevel
{
    /// <summary>
    /// Nivel por default
    /// </summary>
    public LogEventLevel Default { get; set; }

    /// <summary>
    /// Sobreescrituras
    /// </summary>
    public IDictionary<string, LogEventLevel> Override { get; set; }
}

/// <summary>
/// Argumento
/// </summary>
public class Args
{
    /// <summary>
    /// Ruta
    /// </summary>
    public string Path { get; set; }
    /// <summary>
    /// Intervalo
    /// </summary>
    public RollingInterval RollingInterval { get; set; }

    /// <summary>
    /// Plantilla de Template
    /// </summary>
    public string OutputTemplate { get; set; }
}

/// <summary>
/// Escribir en
/// </summary>
public class WriteTo
{
    /// <summary>
    /// Nombre
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Argumentos
    /// </summary>
    public Args Args { get; set; }
}

