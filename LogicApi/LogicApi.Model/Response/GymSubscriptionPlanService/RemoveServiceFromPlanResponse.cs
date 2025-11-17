namespace LogicApi.Model.Response.GymSubscriptionPlanService;

/// <summary>
/// Respuesta de remover servicio de plan
/// </summary>
public class RemoveServiceFromPlanResponse : IApiBaseResponse
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
    /// Indica si se removió exitosamente
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="success"></param>
    public RemoveServiceFromPlanResponse(bool success)
    {
        Success = success;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public RemoveServiceFromPlanResponse()
    {
    }
}

