using LogicApi.Model.Response.GymBranchSchedule;

namespace LogicApi.Model.Request.GymBranchSchedule;

/// <summary>
/// Solicitud para crear horario de sucursal
/// </summary>
public class CreateGymBranchScheduleRequest : IRequest<CreateGymBranchScheduleResponse>, IApiBaseRequest
{
    /// <summary>
    /// Guid de la sucursal de gimnasio
    /// </summary>
    [Required]
    public Guid GymBranchGuid { get; set; }

    /// <summary>
    /// Id del día de la semana (catálogo)
    /// 1=Lunes, 2=Martes, 3=Miércoles, 4=Jueves, 5=Viernes, 6=Sábado, 7=Domingo
    /// </summary>
    [Required]
    public int DayOfWeekCatalogId { get; set; }

    /// <summary>
    /// Hora de apertura
    /// </summary>
    [Required]
    public TimeSpan OpeningTime { get; set; }

    /// <summary>
    /// Hora de cierre
    /// </summary>
    [Required]
    public TimeSpan ClosingTime { get; set; }

    /// <summary>
    /// Indica si debe visualizarse
    /// </summary>
    public bool IsVisible { get; set; } = true;

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public CreateGymBranchScheduleRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public CreateGymBranchScheduleRequest()
    {
    }
}

