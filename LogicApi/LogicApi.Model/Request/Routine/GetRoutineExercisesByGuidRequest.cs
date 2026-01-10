using LogicApi.Model.Response.Routine;
using Common.WebCommon.Models;
using Common.WebCommon.Attributes.CustomDataAnnotations;

namespace LogicApi.Model.Request.Routine;

/// <summary>
/// Solicitud para obtener ejercicios de rutina por GUID
/// </summary>
public class GetRoutineExercisesByGuidRequest : IApiBaseRequest<GetRoutineExercisesByGuidResponse>
{
    /// <summary>
    /// Guid de la rutina
    /// </summary>
    [Required]
    [ValidateGuid]
    public Guid RoutineGuid { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}
