using Common.WebCommon.Attributes.CustomDataAnnotations;
using LogicAdministratorApi.Model.Response.BranchPlan;
using Common.WebCommon.Models;

namespace LogicAdministratorApi.Model.Request.BranchPlan;

/// <summary>
/// Solicitud para actualizar un plan de sucursal
/// </summary>
public class UpdateBranchPlanRequest : IApiBaseRequest<UpdateBranchPlanResponse>
{
    /// <summary>
    /// GUID del plan de sucursal
    /// </summary>
    [Required]
    [ValidateGuid]
    public Guid BranchPlanGuid { get; set; }

    /// <summary>
    /// Nombre del plan
    /// </summary>
    [Required]
    [StringLength(200)]
    public string Name { get; set; }

    /// <summary>
    /// Código único del plan
    /// </summary>
    [StringLength(50)]
    public string Code { get; set; }

    /// <summary>
    /// Descripción del plan
    /// </summary>
    [StringLength(1000)]
    public string Description { get; set; }

    /// <summary>
    /// Precio del plan
    /// </summary>
    [Required]
    public decimal Price { get; set; }

    /// <summary>
    /// Duración del plan en días
    /// </summary>
    [Required]
    public int DurationDays { get; set; }

    /// <summary>
    /// Precio de inscripción o setup fee
    /// </summary>
    public decimal? EnrollmentFee { get; set; }

    /// <summary>
    /// Estado del registro (Activo/Inactivo)
    /// </summary>
    [Required]
    public bool IsActive { get; set; }

    /// <summary>
    /// Características del plan (reemplaza las existentes)
    /// </summary>
    public List<PlanFeatureInput> Features { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}

