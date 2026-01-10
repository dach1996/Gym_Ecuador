namespace LogicApi.Model.Response.SeriesRecord;

/// <summary>
/// Respuesta de crear registro de serie
/// </summary>
public class CreateSeriesRecordResponse : IApiBaseResponse
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
    /// Guid del registro creado
    /// </summary>
    public Guid SeriesRecordGuid { get; set; }

    /// <summary>
    /// Guid del ejercicio
    /// </summary>
    public Guid ExerciseGuid { get; set; }

    /// <summary>
    /// Nombre del ejercicio
    /// </summary>
    public string ExerciseName { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    public DateTime RegistrationDate { get; set; }
}
