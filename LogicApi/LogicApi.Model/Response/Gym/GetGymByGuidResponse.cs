namespace LogicApi.Model.Response.Gym;

/// <summary>
/// Respuesta de obtener gimnasio por GUID
/// </summary>
public class GetGymByGuidResponse : IApiBaseResponse
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
    /// Datos del gimnasio
    /// </summary>
    public GymDetail Gym { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="gym"></param>
    public GetGymByGuidResponse(GymDetail gym)
    {
        Gym = gym;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetGymByGuidResponse()
    {
    }
}

/// <summary>
/// Detalle completo de gimnasio
/// </summary>
public class GymDetail
{
    /// <summary>
    /// Guid del gimnasio
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Nombre del gimnasio
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Descripción del gimnasio
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Descripción corta del gimnasio
    /// </summary>
    public string ShortDescription { get; set; }

    /// <summary>
    /// Dirección del gimnasio
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Teléfono del gimnasio
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    /// Email del gimnasio
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Sitio web del gimnasio
    /// </summary>
    public string Website { get; set; }

    /// <summary>
    /// Horario de apertura
    /// </summary>
    public TimeSpan? OpeningTime { get; set; }

    /// <summary>
    /// Horario de cierre
    /// </summary>
    public TimeSpan? ClosingTime { get; set; }

    /// <summary>
    /// Latitud para localización
    /// </summary>
    public decimal? Latitude { get; set; }

    /// <summary>
    /// Longitud para localización
    /// </summary>
    public decimal? Longitude { get; set; }

    /// <summary>
    /// Estado del gimnasio
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    public DateTime? DateTimeRegister { get; set; }
}
