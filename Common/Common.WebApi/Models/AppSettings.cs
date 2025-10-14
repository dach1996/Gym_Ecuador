
using Common.WebCommon.Models;
namespace Common.WebApi.Models;

/// <summary>
/// Clase mapeada de App Settings
/// </summary>
public class AppSettingsApi : AppSettingsCommon
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
    /// Configuracíones de Jwt
    /// </summary>
    public List<JwtSetting> JwtSettings { get; set; }

    /// <summary>
    /// Permitir Host
    /// </summary>
    /// <value></value>
    public string AllowedHosts { get; set; }

    /// <summary>
    /// Integridad
    /// </summary>
    /// <value></value>
    public List<IntegrityValidation> IntegrityValidationConfig { get; set; }

    /// <summary>
    /// Validaciones de control para Input
    /// </summary>
    /// <value></value>
    public Dictionary<string, ControlValidationItem> InputControlValidations { get; set; }

    /// <summary>
    /// Configuración de AES
    /// </summary>
    /// <value></value>
    public AesConfiguration AesConfiguration { get; set; }
}

/// <summary>
/// Validaciones de Integridad
/// </summary>
public class IntegrityValidation
{
    /// <summary>
    /// Ifentificador
    /// </summary>
    public string Identifier { get; set; }

    /// <summary>
    /// Activado o Dessactivado
    /// </summary>
    public bool Enable { get; set; }

    /// <summary>
    /// Rutas Excluidas
    /// </summary>
    public IEnumerable<string> PathsExclude { get; set; }
}

/// <summary>
/// Configuraciones Jwt
/// </summary>
public class JwtSetting
{
    /// <summary>
    /// Odentificador
    /// </summary>
    /// <value></value>
    public string Identifier { get; set; }

    /// <summary>
    /// Secreto
    /// </summary>
    /// <value></value>
    public string Secret { get; set; }

    /// <summary>
    /// Propietario
    /// </summary>
    /// <value></value>
    public string Issuer { get; set; }

    /// <summary>
    /// Audiencia
    /// </summary>
    /// <value></value>
    public string Audience { get; set; }

    /// <summary>
    /// Minutos para expiración
    /// </summary>
    /// <value></value>
    public int AccessExpiration { get; set; }
}

/// <summary>
/// Item de Expresión Regutlar
/// </summary>
public class ControlValidationItem
{
    /// <summary>
    /// Validaciones conformado por una expresión regular y un mensaje a mostrar
    /// </summary>
    /// <value></value>
    public IDictionary<string, string> Validations { get; set; }

    /// <summary>
    /// Restricciones que no muestran mensaje
    /// </summary>
    /// <value></value>
    public IEnumerable<string> Restrictions { get; set; }
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