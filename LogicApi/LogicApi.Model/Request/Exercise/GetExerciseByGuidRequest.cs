using LogicApi.Model.Response.Exercise;
using Common.WebCommon.Models;
using Common.WebCommon.Attributes.CustomDataAnnotations;

namespace LogicApi.Model.Request.Exercise;

/// <summary>
/// Solicitud para obtener detalle de ejercicio por GUID
/// </summary>
public class GetExerciseByGuidRequest : IApiBaseRequest<GetExerciseByGuidResponse>
{
    /// <summary>
    /// Guid del ejercicio
    /// </summary>
    [Required]
    [ValidateGuid]
    public Guid ExerciseGuid { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}
