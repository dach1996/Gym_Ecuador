namespace LogicApi.Model.Response.Routine;

/// <summary>
/// Respuesta de obtener ejercicios de rutina por GUID
/// </summary>
public class GetRoutineExercisesByGuidResponse : IApiBaseResponse
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
    /// Detalle de la rutina con ejercicios
    /// </summary>
    public RoutineDetail Routine { get; set; }
}

/// <summary>
/// Detalle completo de rutina con ejercicios
/// </summary>
public class RoutineDetail
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
    /// Lista de ejercicios de la rutina
    /// </summary>
    public List<RoutineExerciseDetail> Exercises { get; set; }
}

/// <summary>
/// Detalle de ejercicio en rutina
/// </summary>
public class RoutineExerciseDetail
{
    /// <summary>
    /// Guid del ejercicio
    /// </summary>
    public Guid ExerciseGuid { get; set; }

    /// <summary>
    /// Nombre del ejercicio
    /// </summary>
    public string ExerciseName { get; set; }

    /// <summary>
    /// Número de series
    /// </summary>
    public int Series { get; set; }

    /// <summary>
    /// Desde cuántas repeticiones
    /// </summary>
    public int RepetitionsFrom { get; set; }

    /// <summary>
    /// Hasta cuántas repeticiones
    /// </summary>
    public int RepetitionsTo { get; set; }

    /// <summary>
    /// Segundos de descanso
    /// </summary>
    public int RestSeconds { get; set; }

    /// <summary>
    /// Día de la semana en que se debe realizar el ejercicio (1-7, donde 1=Lunes, 7=Domingo)
    /// </summary>
    public byte Day { get; set; }
}
