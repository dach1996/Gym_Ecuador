using Common.WebCommon.Attributes.CustomDataAnnotations;
using LogicAdministratorApi.Model.Response.ClientMembership;
using Common.WebCommon.Models;

namespace LogicAdministratorApi.Model.Request.ClientMembership;

/// <summary>
/// Solicitud para obtener membresías de clientes paginadas
/// </summary>
public class GetClientMembershipsRequest : IApiBaseRequest<GetClientMembershipsResponse>
{
    /// <summary>
    /// GUID de la persona
    /// </summary>
    [ValidateGuid]
    public Guid PersonGuid { get; set; }

    /// <summary>
    /// GUID del gimnasio
    /// </summary>
    [ValidateGuid]
    public Guid? GymGuid { get; set; }

    /// <summary>
    /// GUID de la sucursal de gimnasio
    /// </summary>
    [ValidateGuid]
    public Guid? GymBranchGuid { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}
