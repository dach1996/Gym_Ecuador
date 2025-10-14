using Common.WebCommon.Models;

namespace Common.WebJob.Model;
/// <summary>
/// AppSettings para WebJobs
/// </summary>
public class AppSettingsWebJob : AppSettingsCommon
{
    /// <summary>
    /// Logs Customizados
    /// </summary>
    /// <value></value>
    public CustomLog CustomLog { get; set; }
}

/// <summary>
/// Logs Customizados
/// </summary>
public class CustomLog
{
    /// <summary>
    /// Verifica si debe Logear los mensajes directos del Queue
    /// </summary>
    /// <value></value>
    public bool QueueMessage { get; set; }
}