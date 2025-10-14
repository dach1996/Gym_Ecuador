namespace Common.Templates.Configurations.Types;

/// <summary>
/// Configuración de Notificaciones
/// </summary>
public class NotificationConfiguration : ConfigurationBase
{
    /// <summary>
    /// Título
    /// </summary>
    /// <value></value>
    public string Title { get; set; }

    /// <summary>
    /// Cuerpo
    /// </summary>
    /// <value></value>
    public string Body { get; set; }
}