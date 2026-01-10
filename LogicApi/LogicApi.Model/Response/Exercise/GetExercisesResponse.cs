using LogicCommon.Model.Response.File;

namespace LogicApi.Model.Response.Exercise;

/// <summary>
/// Respuesta de obtener ejercicios
/// </summary>
public class GetExercisesResponse : IPaginatorApiResponse<ExerciseItem>
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
    public GetExercisesResponse(int totalRegister, IEnumerable<ExerciseItem> registers)
    {
        TotalRegister = totalRegister;
        Registers = registers;
    }

    /// <summary>
    /// Registros
    /// </summary>
    public IEnumerable<ExerciseItem> Registers { get; set; }
}

/// <summary>
/// Item de ejercicio
/// </summary>
public class ExerciseItem
{
    /// <summary>
    /// Guid del ejercicio
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Nombre del ejercicio
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Descripción del ejercicio
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// URL de la imagen del ejercicio
    /// </summary>
    public FileUrlResponse ImageUrl { get; set; }

    /// <summary>
    /// Lista de tags/categorías del ejercicio
    /// </summary>
    public List<string> Tags { get; set; }
}
