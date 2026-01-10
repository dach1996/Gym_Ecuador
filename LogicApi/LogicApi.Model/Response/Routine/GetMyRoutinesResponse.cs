namespace LogicApi.Model.Response.Routine;

/// <summary>
/// Respuesta de obtener mis rutinas
/// </summary>
public class GetMyRoutinesResponse : IPaginatorApiResponse<RoutineItem>
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
    public int TotalRegister { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="totalRegister"></param>
    /// <param name="registers"></param>
    public GetMyRoutinesResponse(int totalRegister, IEnumerable<RoutineItem> registers)
    {
        TotalRegister = totalRegister;
        Registers = registers;
    }

    /// <summary>
    /// Registros
    /// </summary>
    public IEnumerable<RoutineItem> Registers { get; set; }
}

/// <summary>
/// Item de rutina
/// </summary>
public class RoutineItem
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
    /// Cantidad de ejercicios en la rutina
    /// </summary>
    public int ExerciseCount { get; set; }
}
