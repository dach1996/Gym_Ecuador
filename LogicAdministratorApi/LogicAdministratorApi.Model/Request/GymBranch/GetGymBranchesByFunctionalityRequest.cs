using Common.WebApi.Models.Enum;
using Common.WebCommon.Models;
using LogicAdministratorApi.Model.Response.GymBranch;

namespace LogicAdministratorApi.Model.Request.GymBranch;

/// <summary>
/// Solicitud para obtener sucursales de gimnasio por funcionalidad
/// </summary>
public class GetGymBranchesByFunctionalityRequest : IApiBaseRequest<GetGymBranchesByFunctionalityResponse>
{
    /// <summary>
    /// Operación/Funcionalidad para filtrar las sucursales
    /// </summary>
    [Required]
    public OperationAdministratorName Functionality { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }

}

/// <summary>
/// Alcance del rol para la funcionalidad
/// </summary>
public enum FunctionalityRoleScope
{
    /// <summary>
    /// General
    /// </summary>
    General = 1,
}