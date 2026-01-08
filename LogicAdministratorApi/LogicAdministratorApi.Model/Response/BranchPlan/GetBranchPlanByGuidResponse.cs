namespace LogicAdministratorApi.Model.Response.BranchPlan;

/// <summary>
/// Respuesta de obtener plan de sucursal por GUID
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="branchPlan"></param>
public class GetBranchPlanByGuidResponse(BranchPlanDetail branchPlan) : IApiBaseResponse
{
    /// <summary>
    /// Mensaje al Usuario
    /// </summary>
    public string UserMessage { get; set; }

    /// <summary>
    /// Mostrar Mensaje?
    /// </summary>
    public bool ShowMessage { get; set; }

    /// <summary>
    /// Detalle del plan de sucursal
    /// </summary>
    public BranchPlanDetail BranchPlan { get; set; } = branchPlan;
}

/// <summary>
/// Detalle del plan de sucursal
/// </summary>
public class BranchPlanDetail
{
    /// <summary>
    /// Guid del plan de sucursal
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Guid de la sucursal de gimnasio
    /// </summary>
    public Guid GymBranchGuid { get; set; }

    /// <summary>
    /// Nombre del plan
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Código único del plan
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Descripción del plan
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Precio del plan
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Duración del plan en días
    /// </summary>
    public int? DurationDays { get; set; }

    /// <summary>
    /// Precio de inscripción o setup fee
    /// </summary>
    public decimal? EnrollmentFee { get; set; }

    /// <summary>
    /// Estado del registro
    /// </summary>
    public bool IsActive { get; set; }
}

