using Common.WebCommon.Attributes.CustomDataAnnotations;
using LogicAdministratorApi.Model.Response.BranchPlan;
using Common.WebCommon.Models;

namespace LogicAdministratorApi.Model.Request.BranchPlan;

/// <summary>
/// Solicitud para eliminar un plan de sucursal
/// </summary>
public class DeleteBranchPlanRequest : IApiBaseRequest<DeleteBranchPlanResponse>
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

