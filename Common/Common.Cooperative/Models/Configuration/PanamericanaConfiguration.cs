namespace Common.Cooperative.Models.Configuration;

/// <summary>
/// Configuraciòn de Cooperativa
/// </summary>
public class PanamericanaConfiguration
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