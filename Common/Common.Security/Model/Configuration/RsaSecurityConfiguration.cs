namespace Common.Security.Model.Configuration;


/// <summary>
/// Configuraicón de RsaSecurity
/// </summary>
public class RsaSecurityConfiguration
{
    /// <summary>
    /// Implementación
    /// </summary>
    /// <value></value>
    public string CurrentImplementation { get; set; }
}

/// <summary>
/// Información de Configuración de RsaSecurity por Tipo
/// </summary>
/// <typeparam name="T"></typeparam>
public class RsaSecurityConfiguration<T> : RsaSecurityConfiguration
{
    /// <summary>
    /// Implementaciones
    /// </summary>
    /// <value></value>
    public IEnumerable<Implementation<T>> Implementations { get; set; }
}

/// <summary>
/// Configuración de implementación con Identificador
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
/// Configuración de implementación
/// </summary>
public class Implementation<T> : Implementation
{
    /// <summary>
    /// Información
    /// </summary>
    /// <value></value>
    public T Information { get; set; }
}