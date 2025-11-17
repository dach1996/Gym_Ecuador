namespace LogicApi.Model.Response.Service;

/// <summary>
/// Respuesta de obtener servicio por ID
/// </summary>
public class GetServiceByIdResponse : IApiBaseResponse
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
    /// Datos del servicio
    /// </summary>
    public ServiceDetail Service { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="service"></param>
    public GetServiceByIdResponse(ServiceDetail service)
    {
        Service = service;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetServiceByIdResponse()
    {
    }
}

/// <summary>
/// Detalle completo de servicio
/// </summary>
public class ServiceDetail
{
    /// <summary>
    /// Id del servicio
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre del servicio
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Descripción del servicio
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Requiere reserva previa
    /// </summary>
    public bool RequiresReservation { get; set; }

    /// <summary>
    /// Estado activo
    /// </summary>
    public bool IsActive { get; set; }
}

