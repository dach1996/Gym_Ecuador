namespace LogicAdministratorApi.Model.CommonModels;
/// <summary>
/// Horario
/// </summary>
public class ScheduleEstablishmentBranchItem
{
    /// <summary>
    /// Día de la Semana
    /// </summary>
    /// <value></value>
    [Required]
    public string DayOfWeekCode { get; set; }

    /// <summary>
    ///  Horario Apertura
    /// </summary>
    /// <value></value>
    [Required]
    public TimeSpan OpenSchedule { get; set; }

    /// <summary>
    ///  Horario Cierre
    /// </summary> 
    /// <value></value>
    [Required]
    public TimeSpan CloseSchedule { get; set; }
}