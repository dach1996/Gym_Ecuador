namespace LogicApi.Model.Response.ProcessTracking;

/// <summary>
/// Respuesta de actualizar seguimiento de proceso
/// </summary>
public class UpdateProcessTrackingResponse : IApiBaseResponse
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
