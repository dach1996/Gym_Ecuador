namespace LogicApi.Model.Response.Routine;

/// <summary>
/// Respuesta de crear rutina con ejercicios
/// </summary>
public class CreateRoutineWithExercisesResponse : IApiBaseResponse
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
    /// Guid de la rutina creada
    /// </summary>
    public Guid RoutineGuid { get; set; }

    /// <summary>
    /// Nombre de la rutina creada
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Cantidad de ejercicios agregados
    /// </summary>
    public int ExercisesCount { get; set; }
}
