using LogicApi.Model.Response.GymBranch;

using Common.WebCommon.Models;
using Common.WebCommon.Attributes.CustomDataAnnotations;
namespace LogicApi.Model.Request.GymBranch;

/// <summary>
/// Solicitud para obtener detalle de sucursal de gimnasio por GUID
/// </summary>
public class GetGymBranchByGuidRequest : IApiBaseRequest<GetGymBranchByGuidResponse>
{
    /// <summary>
    /// Guid de la sucursal de gimnasio
    /// </summary>
    [Required]
    [ValidateGuid]
    public Guid GymBranchGuid { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}

