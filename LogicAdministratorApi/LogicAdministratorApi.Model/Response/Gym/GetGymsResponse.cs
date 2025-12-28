namespace LogicAdministratorApi.Model.Response.Gym;

/// <summary>
/// Respuesta de obtener gimnasios paginados
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="totalRegister"></param>
/// <param name="registers"></param>
public class GetGymsResponse(int totalRegister, IEnumerable<GymItem> registers) : IPaginatorApiResponse<GymItem>
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
    /// Total de registros
    /// </summary>
    public int TotalRegister { get; set; } = totalRegister;

    /// <summary>
    /// Registros
    /// </summary>
    public IEnumerable<GymItem> Registers { get; set; } = registers;
}

/// <summary>
/// Item de gimnasio
/// </summary>
public class GymItem
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
    /// Código del gimnasio
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Descripción del gimnasio
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Descripción corta del gimnasio
    /// </summary>
    public string ShortDescription { get; set; }

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
    /// Estado del gimnasio
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    public DateTime DateTimeRegister { get; set; }
}

