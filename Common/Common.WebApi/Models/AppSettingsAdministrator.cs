
using Common.WebCommon.Models;
namespace Common.WebApi.Models.AppSettingsModel;

/// <summary>
/// Clase mapeada de App Settings de Administrador
/// </summary>
public class AppSettingsAdministrator : AppSettingsCommon
{
    /// <summary>
    /// Integridad
    /// </summary>
    /// <value></value>
    public List<IntegrityValidation> IntegrityValidationConfig { get; set; }

}
