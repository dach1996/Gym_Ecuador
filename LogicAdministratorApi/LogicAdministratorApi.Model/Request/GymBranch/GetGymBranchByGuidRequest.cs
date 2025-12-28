using Common.WebApi.Models.ContextRequestModel;
using Common.WebCommon.Attributes.CustomDataAnnotations;
using LogicAdministratorApi.Model.Response.GymBranch;
using Common.WebCommon.Models;

namespace LogicAdministratorApi.Model.Request.GymBranch;

/// <summary>
/// Solicitud para obtener detalle de una sucursal de gimnasio por GUID
/// </summary>
public class GetGymBranchByGuidRequest : IApiBaseRequest<GetGymBranchByGuidResponse>
{
    /// <summary>
    /// GUID de la sucursal a obtener
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

