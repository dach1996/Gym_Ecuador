namespace LogicAdministratorApi.Model.Response.Routine;

/// <summary>
/// Respuesta de crear rutina con ejercicios asignada a un usuario
/// </summary>
public class CreateRoutineWithExercisesForUserResponse : IApiBaseResponse
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
    /// Guid del usuario asignado
    /// </summary>
    public Guid UserGuid { get; set; }

    /// <summary>
    /// Cantidad de ejercicios agregados
    /// </summary>
    public int ExercisesCount { get; set; }
}
