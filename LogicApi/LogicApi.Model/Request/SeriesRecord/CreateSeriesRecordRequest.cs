using LogicApi.Model.Response.SeriesRecord;
using Common.WebCommon.Models;
using Common.WebCommon.Attributes.CustomDataAnnotations;

namespace LogicApi.Model.Request.SeriesRecord;

/// <summary>
/// Solicitud para crear registro de serie
/// </summary>
public class CreateSeriesRecordRequest : IApiBaseRequest<CreateSeriesRecordResponse>
{
    /// <summary>
    /// Guid del ejercicio
    /// </summary>
    [Required]
    [ValidateGuid]
    public Guid ExerciseGuid { get; set; }

    /// <summary>
    /// Peso utilizado
    /// </summary>
    [Range(0, double.MaxValue)]
    public decimal? Weight { get; set; }

    /// <summary>
    /// Repeticiones realizadas
    /// </summary>
    [Required]
    [Range(1, int.MaxValue)]
    public int Repetitions { get; set; }

    /// <summary>
    /// Fecha de registro (opcional, por defecto fecha actual)
    /// </summary>
    public DateTime? RegistrationDate { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}
