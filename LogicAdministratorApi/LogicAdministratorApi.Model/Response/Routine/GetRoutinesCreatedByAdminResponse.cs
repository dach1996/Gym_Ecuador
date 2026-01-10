namespace LogicAdministratorApi.Model.Response.Routine;

/// <summary>
/// Respuesta de obtener rutinas creadas por el administrador
/// </summary>
/// <param name="totalRegister"></param>
/// <param name="registers"></param>
public class GetRoutinesCreatedByAdminResponse(int totalRegister, IEnumerable<RoutineAdminItem> registers) : IPaginatorApiResponse<RoutineAdminItem>
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
    public IEnumerable<RoutineAdminItem> Registers { get; set; } = registers;
}

/// <summary>
/// Item de rutina para administrador
/// </summary>
public class RoutineAdminItem
{
    /// <summary>
    /// Guid de la rutina
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Nombre de la rutina
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Fecha de creación
    /// </summary>
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// Guid del usuario asignado
    /// </summary>
    public Guid UserGuid { get; set; }

    /// <summary>
    /// Nombre del usuario asignado
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// Email del usuario asignado
    /// </summary>
    public string UserEmail { get; set; }

    /// <summary>
    /// Cantidad de ejercicios en la rutina
    /// </summary>
    public int ExerciseCount { get; set; }
}
