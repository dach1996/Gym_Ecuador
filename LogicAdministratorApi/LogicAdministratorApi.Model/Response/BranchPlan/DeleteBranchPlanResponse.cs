namespace LogicAdministratorApi.Model.Response.BranchPlan;

/// <summary>
/// Respuesta de eliminar plan de sucursal
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
public class DeleteBranchPlanResponse() : IApiBaseResponse
{
    /// <summary>
    /// Mensaje al Usuario
    /// </summary>
    public string UserMessage { get; set; }

    /// <summary>
    /// Mostrar Mensaje?
    /// </summary>
    public bool ShowMessage { get; set; }
}

