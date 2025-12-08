
using Common.WebCommon.Models;
namespace Common.WebApi.Models;

/// <summary>
/// Clase mapeada de App Settings
/// </summary>
public class AppSettingsApi : AppSettingsCommon
{
 


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
