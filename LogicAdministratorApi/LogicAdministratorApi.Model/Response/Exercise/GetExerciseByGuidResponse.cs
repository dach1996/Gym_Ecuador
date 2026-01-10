using LogicCommon.Model.Response.File;

namespace LogicAdministratorApi.Model.Response.Exercise;

/// <summary>
/// Respuesta de obtener detalle de ejercicio por GUID
/// </summary>
public class GetExerciseByGuidResponse : IApiBaseResponse
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
    /// Detalle completo del ejercicio
    /// </summary>
    public ExerciseDetail Exercise { get; set; }
}

/// <summary>
/// Detalle completo de ejercicio
/// </summary>
public class ExerciseDetail
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
    /// Instrucciones del ejercicio
    /// </summary>
    public string Instructions { get; set; }

    /// <summary>
    /// URL de la imagen del ejercicio
    /// </summary>
    public FileUrlResponse ImageUrl { get; set; }

    /// <summary>
    /// Lista de tags/categorías del ejercicio
    /// </summary>
    public List<ExerciseTagItem> Tags { get; set; }
}

/// <summary>
/// Item de tag/categoría de ejercicio
/// </summary>
public class ExerciseTagItem
{
    /// <summary>
    /// Id del catálogo
    /// </summary>
    public int CatalogId { get; set; }

    /// <summary>
    /// Código del catálogo
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Nombre del tag/categoría
    /// </summary>
    public string Name { get; set; }
}
