namespace LogicAdministratorApi.Model.Response.BranchPlan;

/// <summary>
/// Respuesta de crear plan de sucursal
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="branchPlanGuid"></param>
/// <param name="name"></param>
/// <param name="gymBranchGuid"></param>
public class CreateBranchPlanResponse(Guid branchPlanGuid, string name, Guid gymBranchGuid) : IApiBaseResponse
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
    /// Guid del plan de sucursal creado
    /// </summary>
    public Guid BranchPlanGuid { get; set; } = branchPlanGuid;

    /// <summary>
    /// Nombre del plan
    /// </summary>
    public string Name { get; set; } = name;

    /// <summary>
    /// Guid de la sucursal de gimnasio
    /// </summary>
    public Guid GymBranchGuid { get; set; } = gymBranchGuid;
}

