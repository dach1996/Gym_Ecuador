using Common.PushNotification.Model;

namespace Common.PushNotification.Configuration;

/// <summary>
/// Configuración
/// </summary>
public class PushNotificationConfiguration
{
    /// <summary>
    /// Implementación
    /// </summary>
    /// <value></value>
    public string CurrentImplementation { get; set; }
}

/// <summary>
/// Configuración Genérica
/// </summary>
public class PushNotificationConfiguration<T> : PushNotificationConfiguration
{
    /// <summary>
    /// Implementaciones
    /// </summary>
    /// <value></value>
    public IEnumerable<Implementation<T>> Implementations { get; set; }
}

/// <summary>
/// Configuración de cada Item
/// </summary>
public class Implementation<T>
{
    /// <summary>
    /// Información
    /// </summary>
    /// <value></value>
    public T Information { get; set; }

    /// <summary>
    /// Identificador
    /// </summary>
    /// <value></value>
    public PushNotificationImplementationNames Identifier { get; set; }

}