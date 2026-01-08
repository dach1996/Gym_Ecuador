namespace LogicAdministratorApi.Model.Response.BranchPlan;

/// <summary>
/// Respuesta de actualizar plan de sucursal
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="branchPlanGuid"></param>
/// <param name="name"></param>
public class UpdateBranchPlanResponse(Guid branchPlanGuid, string name) : IApiBaseResponse
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
    /// Guid del plan de sucursal actualizado
    /// </summary>
    public Guid BranchPlanGuid { get; set; } = branchPlanGuid;

    /// <summary>
    /// Nombre del plan
    /// </summary>
    public string Name { get; set; } = name;
}

