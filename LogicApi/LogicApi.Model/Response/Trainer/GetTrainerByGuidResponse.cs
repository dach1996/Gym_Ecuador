namespace LogicApi.Model.Response.Trainer;

/// <summary>
/// Respuesta de obtener entrenador por GUID
/// </summary>
public class GetTrainerByGuidResponse : IApiBaseResponse
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
    /// Datos del entrenador
    /// </summary>
    public TrainerDetail Trainer { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="trainer"></param>
    public GetTrainerByGuidResponse(TrainerDetail trainer)
    {
        Trainer = trainer;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetTrainerByGuidResponse()
    {
    }
}

/// <summary>
/// Detalle completo de entrenador
/// </summary>
public class TrainerDetail
{
    /// <summary>
    /// Guid del entrenador
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Información de la persona
    /// </summary>
    public PersonInfo Person { get; set; }

    /// <summary>
    /// Información del gimnasio
    /// </summary>
    public GymInfo Gym { get; set; }

    /// <summary>
    /// Especialidad del entrenador
    /// </summary>
    public string Specialty { get; set; }

    /// <summary>
    /// Biografía del entrenador
    /// </summary>
    public string Biography { get; set; }

    /// <summary>
    /// URL de foto de perfil
    /// </summary>
    public string ProfilePhotoUrl { get; set; }

    /// <summary>
    /// Estado del entrenador
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    public DateTime? DateTimeRegister { get; set; }
}

/// <summary>
/// Información básica de persona
/// </summary>
public class PersonInfo
{
    /// <summary>
    /// Nombre completo
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Teléfono
    /// </summary>
    public string Phone { get; set; }
}

/// <summary>
/// Información básica de gimnasio
/// </summary>
public class GymInfo
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
    /// Dirección del gimnasio
    /// </summary>
    public string Address { get; set; }
}
