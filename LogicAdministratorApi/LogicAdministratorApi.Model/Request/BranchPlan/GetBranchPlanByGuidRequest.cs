using Common.WebCommon.Attributes.CustomDataAnnotations;
using LogicAdministratorApi.Model.Response.BranchPlan;
using Common.WebCommon.Models;

namespace LogicAdministratorApi.Model.Request.BranchPlan;

/// <summary>
/// Solicitud para obtener un plan de sucursal por GUID
/// </summary>
public class GetBranchPlanByGuidRequest : IApiBaseRequest<GetBranchPlanByGuidResponse>
{
    /// <summary>
    /// GUID del plan de sucursal
    /// </summary>
    [Required]
    [ValidateGuid]
    public Guid BranchPlanGuid { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}

