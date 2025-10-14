namespace Common.EventHub.Models.Configuration.CustomEventHubService;

/// <summary>
/// Configuraciòn de rapidapi 
/// </summary>
public class CustomSignalRConfiguration
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
    /// Api Key
    /// </summary>
    /// <value></value>
    public string ApiKey { get; set; }
}