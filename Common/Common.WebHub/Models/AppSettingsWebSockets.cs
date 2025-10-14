using Common.WebCommon.Models;
namespace Common.WebHub.Models;

/// <summary>
/// Clase mapeada de App Settings
/// </summary>
public class AppSettingsWebSockets : AppSettingsCommon
{
    /// <summary>
    /// Api Key
    /// </summary>
    /// <value></value>
    public string ApiKey { get; set; }

    /// <summary>
    /// Configuración de AES
    /// </summary>
    /// <value></value>
    public AesConfiguration AesConfiguration { get; set; }
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