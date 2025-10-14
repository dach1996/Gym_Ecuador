namespace Common.EventHub.Models.Configuration;

/// <summary>
/// Configuración de servicio 
/// </summary>
public class EventHubConfiguration<T> : EventHubConfiguration
{
    /// <summary>
    /// Implementaciones
    /// </summary>
    /// <value></value>
    public IEnumerable<Implementation<T>> Implementations { get; set; }
}

/// <summary>
/// Configuración de servicio
/// </summary>
public class EventHubConfiguration
{
    /// <summary>
    /// Implementación
    /// </summary>
    /// <value></value>
    public string CurrentImplementation { get; set; }
}

/// <summary>
/// Configuración de servicio
/// </summary>
public class Implementation
{
    /// <summary>
    /// Identificación
    /// </summary>
    /// <value></value>
    public string Identifier { get; set; }

}

/// <summary>
/// Configuración de servicio
/// </summary>
public class Implementation<T> : Implementation
{
    /// <summary>
    /// Información
    /// </summary>
    /// <value></value>
    public T Information { get; set; }

}