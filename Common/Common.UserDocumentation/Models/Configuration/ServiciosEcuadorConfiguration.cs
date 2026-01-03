namespace Common.UserDocumentation.Models.Configuration;

/// <summary>
/// Configuración de ServiciosEcuador
/// </summary>
public class ServiciosEcuadorConfiguration
{
    /// <summary>
    /// Base Url
    /// </summary>
    /// <value></value>
    public string BaseUrl { get; set; }

    /// <summary>
    /// Timeout de Servicio
    /// </summary>
    /// <value></value>
    public int Timeout { get; set; }

    /// <summary>
    /// API Key para autenticación
    /// </summary>
    /// <value></value>
    public string ApiKey { get; set; }
}

