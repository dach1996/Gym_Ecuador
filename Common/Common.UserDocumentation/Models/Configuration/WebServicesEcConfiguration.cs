namespace Common.UserDocumentation.Models.Configuration;

/// <summary>
/// Configuraciòn de rapidapi 
/// </summary>
public class WebServicesEcConfiguration
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
    /// Token de Autorización
    /// </summary>
    /// <value></value>
    public string TokenAuthorization { get; set; }
}