namespace LogicApi.Model.Response.Service;

/// <summary>
/// Respuesta de crear servicio
/// </summary>
public class CreateServiceResponse : IApiBaseResponse
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
    /// Id del servicio creado
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
    public CreateServiceResponse(int serviceId, string name)
    {
        ServiceId = serviceId;
        Name = name;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public CreateServiceResponse()
    {
    }
}

