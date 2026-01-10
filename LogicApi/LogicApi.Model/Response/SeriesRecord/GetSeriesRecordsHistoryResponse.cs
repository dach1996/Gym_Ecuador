namespace LogicApi.Model.Response.SeriesRecord;

/// <summary>
/// Respuesta de obtener historial de registros de series
/// </summary>
public class GetSeriesRecordsHistoryResponse : IPaginatorApiResponse<SeriesRecordItem>
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
    public GetSeriesRecordsHistoryResponse(int totalRegister, IEnumerable<SeriesRecordItem> registers)
    {
        TotalRegister = totalRegister;
        Registers = registers;
    }

    /// <summary>
    /// Registros
    /// </summary>
    public IEnumerable<SeriesRecordItem> Registers { get; set; }
}

/// <summary>
/// Item de registro de serie
/// </summary>
public class SeriesRecordItem
{
    /// <summary>
    /// Guid del registro
    /// </summary>
    public Guid Guid { get; set; }

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

    /// <summary>
    /// Peso utilizado
    /// </summary>
    public decimal? Weight { get; set; }

    /// <summary>
    /// Repeticiones realizadas
    /// </summary>
    public int Repetitions { get; set; }
}
