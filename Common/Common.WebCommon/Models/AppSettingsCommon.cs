namespace Common.WebCommon.Models;
/// <summary>
/// Clase mapeada de App Settings
/// </summary>
public class AppSettingsCommon
{
    /// <summary>
    /// Eliminar header para log
    /// </summary>
    /// <value></value>
    public IEnumerable<string> LogHeadersRemove { get; set; }

    /// <summary>
    /// Logea la información sensible
    /// </summary>
    /// <value></value>
    public bool LogSensitiveInformation { get; set; }

    /// <summary>
    /// Configuración de AES
    /// </summary>
    /// <value></value>
    public AesConfiguration AesConfiguration { get; set; }

    /// <summary>
    /// Configuraciones Generales
    /// </summary>
    /// <value></value>
    public GeneralConfiguration GeneralConfiguration { get; set; }

    /// <summary>
    /// Cadenas de conexión
    /// </summary>

    public List<CustomConnectionStrings> CustomConnectionStrings { get; set; }

    /// <summary>
    /// Configuraciones de Conexión a Blob
    /// </summary>
    public CurrentImplementationBase BlobConfiguration { get; set; }

    /// <summary>
    /// Configuración para Servicios de Tarjetas
    /// </summary>
    /// <value></value>
    public CurrentImplementationBase CardServicesConfiguration { get; set; }

    /// <summary>
    /// Configuración de Servicio de Documentos
    /// </summary>
    /// <value></value>
    public CurrentImplementationBase DocumentationServicesConfiguration { get; set; }

    /// <summary>
    /// Configuración de autenticación
    /// </summary>
    /// <value></value>
    public CurrentImplementationBase AuthenticationServiceConfiguration { get; set; }

    /// <summary>
    /// Configuración de Queue
    /// </summary>
    /// <value></value>
    public CurrentImplementationBase QueueConfiguration { get; set; }

    /// <summary>
    /// Configuración de Queue
    /// </summary>
    /// <value></value>
    public CurrentImplementationBase MailNotificationConfiguration { get; set; }

    /// <summary>
    /// Configuración de Notificaciones Push
    /// </summary>
    /// <value></value>
    public CurrentImplementationBase PushNotificationConfiguration { get; set; }

    /// <summary>
    /// Configuración de eventos hubs
    /// </summary>
    /// <value></value>
    public CurrentImplementationBase EventHubConfiguration { get; set; }

    /// <summary>
    /// Zona Horaria
    /// </summary>
    /// <value></value>
    public string TimeZone { get; set; }

    /// <summary>
    /// Separador Custom
    /// </summary>
    /// <value></value>
    public string CustomSeparator { get; set; }

}


/// <summary>
/// Configuración de AES
/// </summary>
public class AesConfiguration
{
    /// <summary>
    /// Aes para Server
    /// </summary>
    /// <value></value>
    public Dictionary<AesImplementationName, string> Keys { get; set; }

    /// <summary>
    /// Nombres de implementaciones para Aes
    /// </summary>
    public enum AesImplementationName
    {
        ServerGeneral
    }
}


/// <summary>
/// Configuraciones Generales
/// </summary>
public class GeneralConfiguration
{
    /// <summary>
    /// Implementaciòn de contexto a utilizar
    /// </summary>
    /// <value></value>
    public string AddContextImplementation { get; set; }

    /// <summary>
    /// Implementación para configuración contexto
    /// </summary>
    /// <value></value>
    public string ConfigureContextImplementation { get; set; }

    /// <summary>
    /// Implementación para validad Integridad
    /// </summary>
    /// <value></value>
    public string ValidateIntegrityImplementation { get; set; }
}

/// <summary>
/// Connection String
/// </summary>
public class CustomConnectionStrings
{
    /// <summary>
    /// Identificador
    /// </summary>
    public string Identifier { get; set; }

    /// <summary>
    /// Conexión
    /// </summary>
    public string ConnectionString { get; set; }

    /// <summary>
    /// Time Out
    /// </summary>
    public int CommandTimeOut { get; set; }

    /// Cadena de conexión
    /// </summary>
    /// <value></value>
    public bool EnableSensitiveDataLogging { get; set; }

    /// <summary>
    /// Time out 
    /// </summary>
    /// <value></value>    
    public bool EnableDetailedErrors { get; set; }

    /// <summary>
    /// Secreto Aes
    /// </summary>
    /// <value></value>
    public string AesSecret { get; set; }
}

/// <summary>
/// Clase base de CurrentImplementation
/// </summary>
public class CurrentImplementationBase
{
    /// <summary>
    /// Identificador
    /// </summary>
    public string CurrentImplementation { get; set; }
}

