using Common.WebCommon.Models;

namespace LogicApi.Model.Response.ProcessTracking;

/// <summary>
/// Respuesta de obtener seguimientos de procesos
/// </summary>
public class GetProcessTrackingsResponse : IPaginatorResponse<ProcessTrackingItem>
{
    /// <summary>
    /// Lista de seguimientos de procesos
    /// </summary>
    public IEnumerable<ProcessTrackingItem> Registers { get; set; }

    /// <summary>
    /// Total de registros
    /// </summary>
    public int TotalRegister { get; set; }

}

/// <summary>
/// Item de seguimiento de proceso
/// </summary>
public class ProcessTrackingItem
{
    /// <summary>
    /// Guid del seguimiento de proceso
    /// </summary>
    public Guid Guid { get; set; }

     /// <summary>
     /// Fecha de registro
     /// </summary>
     public DateTime RegistrationDate { get; set; }

    /// <summary>
    /// Peso corporal actual (en kg o la unidad estándar)
    /// </summary>
    public decimal Weight { get; set; }

    /// <summary>
    /// Altura de la persona (en cm) - No cambia con frecuencia, pero es clave para el IMC.
    /// </summary>
    public decimal Height { get; set; }

}
