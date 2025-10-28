namespace LogicApi.Model.Response.ProcessTracking;

/// <summary>
/// Respuesta de crear seguimiento de proceso
/// </summary>
public class CreateProcessTrackingResponse : IApiBaseResponse
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
    /// Guid del seguimiento de proceso creado
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
    public CreateProcessTrackingResponse(Guid processTrackingGuid, string processName)
    {
        ProcessTrackingGuid = processTrackingGuid;
        ProcessName = processName;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public CreateProcessTrackingResponse()
    {
    }
}
