namespace Common.Authentication.Models.Configuration;

/// <summary>
/// Configuraciòn de Google
/// </summary>
public class GoogleConfiguration
{
    /// <summary>
    /// Id de cliente
    /// </summary>
    /// <value></value>
    public IEnumerable<string> Audiences { get; set; }
}