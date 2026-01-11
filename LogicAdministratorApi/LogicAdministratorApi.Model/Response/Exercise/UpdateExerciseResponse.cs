namespace LogicAdministratorApi.Model.Response.Exercise;

/// <summary>
/// Respuesta de actualizar ejercicio
/// </summary>
/// <param name="exerciseGuid">Guid del ejercicio actualizado</param>
/// <param name="name">Nombre del ejercicio actualizado</param>
public class UpdateExerciseResponse(Guid exerciseGuid, string name) : IApiBaseResponse
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
    /// Guid del ejercicio actualizado
    /// </summary>
    public Guid ExerciseGuid { get; set; } = exerciseGuid;

    /// <summary>
    /// Nombre del ejercicio actualizado
    /// </summary>
    public string Name { get; set; } = name;
}
