using Common.Security.Model.Enum;

namespace Common.Security.Model;

/// <summary>
/// Modelo para generación de Token
/// </summary>
public class JwtSettings
{
    /// <summary>
    /// Identificador
    /// </summary>
    /// <value></value>
    public JwtIdentifier Identifier { get; set; }

    /// <summary>
    /// Secreto
    /// </summary>
    public string Secret { get; set; }

    /// <summary>
    /// Usuario
    /// </summary>
    public string Issuer { get; set; }

    /// <summary>
    /// Audiencia
    /// </summary>
    public string Audience { get; set; }

    /// <summary>
    /// Expiración en minutos
    /// </summary>
    public int AccessExpiration { get; set; }

}
