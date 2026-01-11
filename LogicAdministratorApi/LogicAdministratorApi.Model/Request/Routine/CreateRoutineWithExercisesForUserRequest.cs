using LogicAdministratorApi.Model.Response.Routine;
using Common.WebCommon.Models;
using Common.WebCommon.Attributes.CustomDataAnnotations;

namespace LogicAdministratorApi.Model.Request.Routine;

/// <summary>
/// Solicitud para crear rutina con ejercicios asignada a un usuario
/// </summary>
public class CreateRoutineWithExercisesForUserRequest : IApiBaseRequest<CreateRoutineWithExercisesForUserResponse>
{
    /// <summary>
    /// Guid del usuario al que se asigna la rutina
    /// </summary>
    [Required]
    [ValidateGuid]
    public Guid UserGuid { get; set; }

    /// <summary>
    /// Nombre de la rutina
    /// </summary>
    [Required]
    [StringLength(200)]
    public string Name { get; set; }

    /// <summary>
    /// Lista de ejercicios con sus parámetros
    /// </summary>
    [Required]
    public List<RoutineExerciseItem> Exercises { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}

/// <summary>
/// Item de ejercicio para rutina
/// </summary>
public class RoutineExerciseItem
{
    /// <summary>
    /// Guid del ejercicio
    /// </summary>
    [Required]
    [ValidateGuid]
    public Guid ExerciseGuid { get; set; }

    /// <summary>
    /// Número de series
    /// </summary>
    [Required]
    [Range(1, int.MaxValue)]
    public int Series { get; set; }

    /// <summary>
    /// Desde cuántas repeticiones
    /// </summary>
    [Required]
    [Range(1, int.MaxValue)]
    public int RepetitionsFrom { get; set; }

    /// <summary>
    /// Hasta cuántas repeticiones
    /// </summary>
    [Required]
    [Range(1, int.MaxValue)]
    public int RepetitionsTo { get; set; }

    /// <summary>
    /// Segundos de descanso
    /// </summary>
    [Required]
    [Range(0, int.MaxValue)]
    public int RestSeconds { get; set; }

    /// <summary>
    /// Día de la semana en que se debe realizar el ejercicio (1-7, donde 1=Lunes, 7=Domingo)
    /// </summary>
    [Required]
    [Range(1, 7)]
    public byte Day { get; set; }
}
