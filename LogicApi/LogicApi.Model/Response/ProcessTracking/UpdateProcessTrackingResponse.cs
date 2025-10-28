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

    /// <summary>
    /// Guid del seguimiento de proceso actualizado
    /// </summary>
    public Guid ProcessTrackingGuid { get; set; }

    /// <summary>
    /// Nombre del proceso
    /// </summary>
    public string ProcessName { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="processTrackingGuid"></param>
    /// <param name="processName"></param>
    public UpdateProcessTrackingResponse(Guid processTrackingGuid, string processName)
    {
        ProcessTrackingGuid = processTrackingGuid;
        ProcessName = processName;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public UpdateProcessTrackingResponse()
    {
    }
}
