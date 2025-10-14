namespace Common.Mail.Model.Configuration;

/// <summary>
/// Configuraicón
/// </summary>
public class MailNotificationConfiguration
{
    /// <summary>
    /// Implementación
    /// </summary>
    /// <value></value>
    public string CurrentImplementation { get; set; }

    /// <summary>
    /// Correo por default que sea el origen
    /// </summary>
    /// <value></value>
    public string DefaultFrom { get; set; }

    /// <summary>
    /// Correos ocultos por default
    /// </summary>
    /// <value></value>
    public IEnumerable<string> DefaultBccs { get; set; }
}

/// <summary>
/// Información de Configuración de Tipo
/// </summary>
/// <typeparam name="T"></typeparam>
public class MailNotificationConfiguration<T> : MailNotificationConfiguration
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
