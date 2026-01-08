namespace LogicAdministratorApi.Model.Response.BranchPlan;

/// <summary>
/// Respuesta de obtener planes de sucursal paginados
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="totalRegister"></param>
/// <param name="registers"></param>
public class GetBranchPlansResponse(int totalRegister, IEnumerable<BranchPlanItem> registers) : IPaginatorApiResponse<BranchPlanItem>
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
    /// Total de registros
    /// </summary>
    public int TotalRegister { get; set; } = totalRegister;

    /// <summary>
    /// Registros
    /// </summary>
    public IEnumerable<BranchPlanItem> Registers { get; set; } = registers;
}

/// <summary>
/// Item de plan de sucursal
/// </summary>
public class BranchPlanItem
{
    /// <summary>
    /// Guid del plan de sucursal
    /// </summary>
    public Guid Guid { get; set; }

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

