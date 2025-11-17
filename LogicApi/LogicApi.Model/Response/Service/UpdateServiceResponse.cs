namespace LogicApi.Model.Response.Service;

/// <summary>
/// Respuesta de actualizar servicio
/// </summary>
public class UpdateServiceResponse : IApiBaseResponse
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
    /// Id del servicio
    /// </summary>
    public int ServiceId { get; set; }

    /// <summary>
    /// Nombre del servicio
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="serviceId"></param>
    /// <param name="name"></param>
    public UpdateServiceResponse(int serviceId, string name)
    {
        ServiceId = serviceId;
        Name = name;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public UpdateServiceResponse()
    {
    }
}

