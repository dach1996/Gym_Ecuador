namespace Common.Card.Models.Configuration;

/// <summary>
/// Configuraciòn de rapidapi 
/// </summary>
public class RapidapiConfiguration
{
    /// <summary>
    /// Base Url
    /// </summary>
    /// <value></value>
    public string BaseUrl { get; set; }

    /// <summary>
    /// Headers
    /// </summary>
    /// <value></value>
    public IDictionary<string, string> Headers { get; set; }
}