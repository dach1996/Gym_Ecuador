namespace Common.Security.Model.Configuration.File;
/// <summary>
/// Información de archivo
/// </summary>
public class FileRsaSecurityConfiguration
{
    /// <summary>
    /// Llave Privada
    /// </summary>
    /// <value></value>
    public string PrivateKeyBase64 { get; set; }

    /// <summary>
    /// Llave pública
    /// </summary>
    /// <value></value>
    public string PublicKeyBase64 { get; set; }
}

/// <summary>
/// Información de llave
/// </summary>
public class KeyInformation
{
    /// <summary>
    /// Llave Privada
    /// </summary>
    /// <value></value>
    public string PrivateKey { get; set; }

    /// <summary>
    /// Llave pública
    /// </summary>
    /// <value></value>
    public string PublicKey { get; set; }
}