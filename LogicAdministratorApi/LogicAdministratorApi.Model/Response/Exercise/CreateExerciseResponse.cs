namespace LogicAdministratorApi.Model.Response.Exercise;

/// <summary>
/// Respuesta de crear ejercicio
/// </summary>
/// <param name="exerciseGuid">Guid del ejercicio creado</param>
/// <param name="name">Nombre del ejercicio creado</param>
public class CreateExerciseResponse(Guid exerciseGuid, string name) : IApiBaseResponse
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
    /// Guid del ejercicio creado
    /// </summary>
    public Guid ExerciseGuid { get; set; } = exerciseGuid;

    /// <summary>
    /// Nombre del ejercicio creado
    /// </summary>
    public string Name { get; set; } = name;
}
